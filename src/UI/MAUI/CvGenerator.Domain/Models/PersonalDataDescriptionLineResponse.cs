using System.ComponentModel.DataAnnotations;

namespace CvGenerator.Domain.Models
{
    public class PersonalDataDescriptionLineResponse
    {
        public int Id { get; set; }

        public string Line { get; set; } = string.Empty;

        public int PersonalDataId { get; set; }

        [Required]
        public int Priority { get; set; }
    }
}
