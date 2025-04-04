using System.ComponentModel.DataAnnotations;

namespace CvGenerator.Domain.Models
{
    public class BasicPersonalDataResponse
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public IEnumerable<PersonalDataDescriptionLineResponse> PersonalDataDescriptionLines { get; set; } = [];

        [Required]
        public string PhotoPath { get; set; } = string.Empty;
    }
}
