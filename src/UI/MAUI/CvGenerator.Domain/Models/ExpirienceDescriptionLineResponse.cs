using System.ComponentModel.DataAnnotations;

namespace CvGenerator.Domain.Models
{
    public class ExpirienceDescriptionLineResponse
    {
        public int Id { get; set; }

        public string Line { get; set; } = string.Empty;

        public int ExpirienceId { get; set; }

        public ExpirienceResponse Education { get; set; } = default!;

        [Required]
        public int Priority { get; set; }

    }
}
