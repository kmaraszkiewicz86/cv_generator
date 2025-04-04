using CvGenerator.Shared.Enums;

namespace CvGenerator.Domain.Services.Interfaces
{
    public interface IPdfCvService : IService
    {
        Task<byte[]> CreatePdfAsync(ApplicationLanguageType type);
    }
}
