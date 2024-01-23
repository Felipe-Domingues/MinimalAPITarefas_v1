using System;
using System.Data;

namespace TarefaApi.Data;
public class TarefaContext
{
    public delegate Task<IDbConnection> GetConnection();
}
