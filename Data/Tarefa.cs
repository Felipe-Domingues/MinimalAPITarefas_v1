using System.ComponentModel.DataAnnotations.Schema;

namespace TarefaApi.Data;

[Table("Tarefas")]
public record Tarefa(int Id, string Atividade, string Status);
