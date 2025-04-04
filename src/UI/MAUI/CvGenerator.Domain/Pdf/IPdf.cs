using CvGenerator.Domain.Models;
using CvGenerator.Shared.Enums;

namespace CvGenerator.Domain.Pdf
{
    public interface IPdfCvGenerator
    {
        Task<byte[]> CreatePdfAsync(ApplicationLanguageType type, BasicPersonalDataResponse basicPersonalDataResponse, ResumeResponse resumeResponse, ContactResponse contactResponse);
    }
}
