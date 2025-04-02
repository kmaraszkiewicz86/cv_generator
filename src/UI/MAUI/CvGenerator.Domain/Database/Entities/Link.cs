namespace CvGenerator.Domain.Database.Entities
{
    public class Link
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public int LinkTypeId { get; set; }
        public LinkType LinkType { get; set; } = default!;
    }


    

}
