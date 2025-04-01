using Microsoft.Data.Sqlite;
using CvGenerator.Domain.Database;

namespace CvGenerator.Infrastructure.Database
{
    public class SqliteConnectionConnection : ISqliteConnectionConnection
    {
        public SqliteConnection Connection { get; }

        public SqliteConnectionConnection(SqliteConnection connection)
        {
            Connection = connection;
        }

        public async ValueTask DisposeAsync()
        {
            await Connection.CloseAsync();
        }
    }
}
