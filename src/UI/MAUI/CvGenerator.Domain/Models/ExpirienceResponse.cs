using System.ComponentModel.DataAnnotations;

namespace CvGenerator.Domain.Models
{
    public class ExpirienceResponse
    {
        public int Id { get; set; }

        [Required]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        public string Position { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string UsedSkills { get; set; } = string.Empty;

        public int ResumeId { get; set; }

        [Required]
        public int Priority { get; set; }

        [Required]
        public ResumeResponse Resume { get; set; } = default!;

        public IEnumerable<ExpirienceDescriptionLineResponse> ExpirienceDescriptionLines { get; set; } = [];
    }
}
