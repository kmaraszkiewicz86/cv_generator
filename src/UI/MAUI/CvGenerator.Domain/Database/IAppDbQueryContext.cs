namespace CvGenerator.Domain.Database
{
    public interface IAppDbQueryContext
    {
        Task<ISqliteConnectionConnection> CreateConnectionAsync();
    }
}
