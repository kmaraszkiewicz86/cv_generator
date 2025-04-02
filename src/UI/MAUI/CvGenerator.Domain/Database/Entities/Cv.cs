namespace CvGenerator.Domain.Database.Entities
{
    public class Cv
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Education> Educations { get; set; } = [];
        public ICollection<Experience> Experiences { get; set; } = [];
        public ICollection<Skill> Skills { get; set; } = [];
        public ICollection<Language> Languages { get; set; } = [];
        public ICollection<Link> Links { get; set; } = [];
        public string Language { get; set; } = string.Empty;
    }
}
