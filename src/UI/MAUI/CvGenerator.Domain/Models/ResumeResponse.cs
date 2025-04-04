using System.ComponentModel.DataAnnotations;

namespace CvGenerator.Domain.Models
{
    public class ResumeResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Residence { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhotoPath { get; set; } = string.Empty;

        [Required]
        [Range(1, 100)]
        public int YearsOfExpirience { get; set; }

        [Required]
        public string CvPath { get; set; } = string.Empty;

        public EducationResponse[] Educations { get; set; } = [];

        public ExpirienceResponse[] Expiriences { get; set; } = [];

        public SkillResponse[] Skills { get; set; } = [];

        public LanguageResponse[] Languages { get; set; } = [];

        public ProjectResponse[] Projects { get; set; } = [];
    }
}
