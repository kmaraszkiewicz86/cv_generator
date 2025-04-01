using FluentResults;

namespace CvGenerator.Domain.Database.Repositories
{
    public interface IUnitOfWork
    {
        Task<Result> SaveChangesAsync();
    }
}
