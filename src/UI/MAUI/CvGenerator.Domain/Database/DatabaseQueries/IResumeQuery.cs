using CvGenerator.Domain.Models;
using CvGenerator.Shared.Enums;

namespace CvGenerator.Domain.Database.DatabaseQueries
{
    public interface IResumeQuery : IQuery
    {
        Task<ResumeResponse> GetAsync(ApplicationLanguageType type);
    }
}
