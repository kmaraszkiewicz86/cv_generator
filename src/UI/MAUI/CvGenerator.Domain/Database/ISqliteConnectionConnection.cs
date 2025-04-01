using Microsoft.Data.Sqlite;

namespace CvGenerator.Domain.Database
{
    public interface ISqliteConnectionConnection: IAsyncDisposable
    {
        SqliteConnection Connection { get; }
    }
}
