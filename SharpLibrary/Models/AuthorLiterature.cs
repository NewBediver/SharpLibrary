namespace SharpLibrary.Models
{
    public class AuthorLiterature
    {
        public long AuthorId { get; set; }
        public Author Author { get; set; }

        public long LiteratureId { get; set; }
        public Literature Literature { get; set; }
    }
}
