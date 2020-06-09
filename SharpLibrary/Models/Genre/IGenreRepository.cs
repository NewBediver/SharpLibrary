using System.Linq;

namespace SharpLibrary.Models
{
    public interface IGenreRepository
    {
        IQueryable<Genre> Genres { get; }
        void SaveGenre(Genre genre);
        Genre DeleteGenre(long genreId);
    }
}
