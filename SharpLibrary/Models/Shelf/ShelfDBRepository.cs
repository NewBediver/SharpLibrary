using System.Linq;

namespace SharpLibrary.Models
{
    public class ShelfDBRepository : IShelfRepository
    {
        private ApplicationDBContext _context;

        public ShelfDBRepository(ApplicationDBContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Shelf> Shelves => _context.Shelves;

        public Shelf DeleteShelf(long shelfId)
        {
            Shelf dbEntry = _context.Shelves.FirstOrDefault(elm => elm.Id == shelfId);
            if (dbEntry != null)
            {
                _context.Shelves.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveShelf(Shelf shelf)
        {
            if (shelf.Id == 0)
            {
                _context.Shelves.Add(shelf);
            }
            else
            {
                Shelf dbEntry = _context.Shelves.FirstOrDefault(elm => elm.Id == shelf.Id);
                if (dbEntry != null)
                {
                    dbEntry.Number = shelf.Number;
                    dbEntry.RackId = shelf.RackId;
                }
            }
            _context.SaveChanges();
        }
    }
}
