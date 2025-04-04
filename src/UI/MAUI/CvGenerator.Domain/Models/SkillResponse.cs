using System.ComponentModel.DataAnnotations;

namespace CvGenerator.Domain.Models
{
    public class SkillResponse
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Precentages { get; set; }

        public int ResumeId { get; set; }

        [Required]
        public ResumeResponse Resume { get; set; } = default!;

        [Required]
        public int Priority { get; set; }
    }
}
