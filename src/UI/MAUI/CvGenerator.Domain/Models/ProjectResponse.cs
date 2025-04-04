using System.ComponentModel.DataAnnotations;

namespace CvGenerator.Domain.Models
{
    public class ProjectResponse
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string SkillsUsed { get; set; } = string.Empty;

        public int ResumeId { get; set; }

        public ProjectDescriptionLineResponse[] DescriptionLines { get; set; } = [];

        public int Priority { get; set; }
    }
}
