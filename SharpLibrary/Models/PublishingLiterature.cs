namespace SharpLibrary.Models
{
    public class PublishingLiterature
    {
        public long PublishingId { get; set; }
        public Publishing Publishing { get; set; }

        public long LiteratureId { get; set; }
        public Literature Literature { get; set; }
    }
}
