using Npgsql;
using System.Data;

namespace Negocie._3_Integracoes
{
    public class DbConnection
    {
        public static IDbConnection CreateConnection(string connectionString)
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}
