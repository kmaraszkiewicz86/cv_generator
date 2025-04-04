namespace CvGenerator.Domain.Models
{
    public class ProjectDescriptionLineResponse
    {
        public int Id { get; set; }

        public string Line { get; set; } = string.Empty;

        public int ProjectId { get; set; }

        public int Priority { get; set; }
    }
}
