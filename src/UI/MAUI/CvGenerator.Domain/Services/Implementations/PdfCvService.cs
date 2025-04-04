using CvGenerator.Domain.Database.DatabaseQueries;
using CvGenerator.Domain.Models;
using CvGenerator.Domain.Pdf;
using CvGenerator.Domain.Services.Interfaces;
using CvGenerator.Shared.Enums;

namespace CvGenerator.Domain.Services.Implementations
{
    public class PdfCvService(
        IPersonalDataQuery _personalDataService, 
        IResumeQuery _resumeService,
        IPdfCvGenerator _pdfCvGenerator) : IPdfCvService
    {
        public async Task<byte[]> CreatePdfAsync(ApplicationLanguageType type)
        {
            BasicPersonalDataResponse basicPersonalDataResponse = await _personalDataService.GetBasicDataAsync(type);
            ResumeResponse resumeResponse = (await _resumeService.GetAsync(type))!;
            ContactResponse contactResponse = await _personalDataService.GetContactDataAsync();

            return await _pdfCvGenerator.CreatePdfAsync(type, basicPersonalDataResponse, resumeResponse, contactResponse);
        }
    }
}
