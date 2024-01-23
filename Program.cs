using TarefaApi.Endpoints;
using TarefaApi.Extensions;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);
builder.AddPersistence();
var app = builder.Build();


app.MapTarefasEndpoints();

app.Run();
