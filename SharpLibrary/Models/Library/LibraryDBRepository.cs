using System.Linq;

namespace SharpLibrary.Models
{
    public class LibraryDBRepository : ILibraryRepository
    {
        private ApplicationDBContext _context;

        public LibraryDBRepository(ApplicationDBContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Library> Libraries => _context.Libraries;

        public Library DeleteLibrary(long libraryId)
        {
            Library dbEntry = _context.Libraries.FirstOrDefault(elm => elm.Id == libraryId);
            if (dbEntry != null)
            {
                _context.Libraries.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveLibrary(Library library)
        {
            if (library.Id == 0)
            {
                _context.Libraries.Add(library);
            }
            else
            {
                Library dbEntry = _context.Libraries.FirstOrDefault(elm => elm.Id == library.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = library.Name;
                    dbEntry.Description = library.Description;
                }
            }
            _context.SaveChanges();
        }
    }
}
