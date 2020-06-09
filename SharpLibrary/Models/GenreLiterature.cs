namespace SharpLibrary.Models
{
    public class GenreLiterature
    {
        public long GenreId { get; set; }
        public Genre Genre { get; set; }

        public long LiteratureId { get; set; }
        public Literature Literature { get; set; }
    }
}
