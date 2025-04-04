namespace CvGenerator.Domain.Models
{
    public class LanguageResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Level { get; set; } = string.Empty;

        public int Priority { get; set; }
    }
}
