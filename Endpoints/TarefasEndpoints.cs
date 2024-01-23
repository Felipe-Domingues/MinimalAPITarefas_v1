using System;
using Dapper.Contrib.Extensions;
using TarefaApi.Data;
using static TarefaApi.Data.TarefaContext;

namespace TarefaApi.Endpoints;
public static class TarefasEndpoints
{
    public static void MapTarefasEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => $"Bem-vindo a API Tarefas - {DateTime.Now}");

        app.MapGet("/tarefas", async (GetConnection connectionGetter) =>
        {
            using var con = await connectionGetter();
            var tarefas = con.GetAll<Tarefa>().ToList();
            if (tarefas is null)
                return Results.NotFound("Tarefas não encontradas...");

            return Results.Ok(tarefas);
        });

        app.MapGet("/tarefas/{id:int}", async (GetConnection connectionGetter, int id) =>
        {
            using var con = await connectionGetter();
            return con.Get<Tarefa>(id) is Tarefa tarefa ? Results.Ok(tarefa) : Results.NotFound($"Tarefa {id} não econtrada...");
        });

        app.MapPost("/tarefas", async (GetConnection connectionGetter, Tarefa tarefa) =>
        {
            if (tarefa is null)
                return Results.BadRequest("Dados inválidos");
            using var con = await connectionGetter();
            var id = con.Insert(tarefa);
            return Results.Created($"/tarefas/{id}", tarefa);
        });

        app.MapPut("/tarefas", async (GetConnection connectionGetter, Tarefa tarefa) =>
        {
            if (tarefa is null)
                return Results.BadRequest("Dados inválidos");
            using var con = await connectionGetter();
            con.Update(tarefa);
            return Results.Ok(tarefa);
        });

        app.MapDelete("/tarefas/{id:int}", async (GetConnection connectionGetter, int id) =>
       {
           using var con = await connectionGetter();
           var tarefaToBeDeleted = con.Get<Tarefa>(id);

           if (tarefaToBeDeleted is null)
               return Results.NotFound($"Tarefa {id} não encontrada...");

           con.Delete(tarefaToBeDeleted);

           return Results.Ok(tarefaToBeDeleted);
       });
    }
}
