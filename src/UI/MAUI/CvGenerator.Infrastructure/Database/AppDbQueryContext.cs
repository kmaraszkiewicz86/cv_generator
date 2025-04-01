using Microsoft.Data.Sqlite;
using CvGenerator.Domain.Database;

namespace CvGenerator.Infrastructure.Database
{
    public class AppDbQueryContext(string connectionString) : IAppDbQueryContext
    {
        public async Task<ISqliteConnectionConnection> CreateConnectionAsync()
        {
            SqliteConnection connection = new (connectionString);
            await connection.OpenAsync();

            return new SqliteConnectionConnection(connection);
        }
    }
}
