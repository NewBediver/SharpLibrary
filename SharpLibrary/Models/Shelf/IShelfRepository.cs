using System.Linq;

namespace SharpLibrary.Models
{
    public interface IShelfRepository
    {
        IQueryable<Shelf> Shelves { get; }
        void SaveShelf(Shelf shelf);
        Shelf DeleteShelf(long shelfId);
    }
}
