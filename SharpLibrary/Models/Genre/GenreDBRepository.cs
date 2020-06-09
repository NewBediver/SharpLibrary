using System.Linq;

namespace SharpLibrary.Models
{
    public class GenreDBRepository : IGenreRepository
    {
        private ApplicationDBContext _context;

        public GenreDBRepository(ApplicationDBContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Genre> Genres => _context.Genres;

        public void SaveGenre(Genre genre)
        {
            if (genre.Id == 0)
            {
                _context.Genres.Add(genre);
            }
            else
            {
                Genre dbEntry = _context.Genres.FirstOrDefault(elm => elm.Id == genre.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = genre.Name;
                    dbEntry.Description = genre.Description;
                }
            }
            _context.SaveChanges();
        }

        public Genre DeleteGenre(long genreId)
        {
            Genre dbEntry = _context.Genres.FirstOrDefault(elm => elm.Id == genreId);
            if (dbEntry != null)
            {
                _context.Genres.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
