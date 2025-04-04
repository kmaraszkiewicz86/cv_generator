using System.ComponentModel.DataAnnotations;

namespace CvGenerator.Domain.Models
{
    public class EducationResponse
    {
        public int Id { get; set; }

        [Required]
        public string SpecializationName { get; set; } = string.Empty;

        [Required]
        public string SchoolName { get; set; } = string.Empty;

        [Required]
        public int StartYear { get; set; }

        public int? EndYear { get; set; }

        public int ResumeId { get; set; }

        [Required]
        public ResumeResponse Resume { get; set; } = default!;

        [Required]
        public int Priority { get; set; }

        public string EducationDescription { get; set; } = string.Empty;
    }
}
