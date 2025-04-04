using CvGenerator.Domain.Models;
using CvGenerator.Shared.Enums;

namespace CvGenerator.Domain.Database.DatabaseQueries
{
    public interface IPersonalDataQuery : IQuery
    {
        Task<BasicPersonalDataResponse> GetBasicDataAsync(ApplicationLanguageType type);
        Task<ContactResponse> GetContactDataAsync();
    }
}
