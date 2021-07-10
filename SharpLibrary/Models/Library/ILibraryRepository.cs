using System.Linq;

namespace SharpLibrary.Models
{
    public interface ILibraryRepository
    {
        IQueryable<Library> Libraries { get; }
        void SaveLibrary(Library library);
        Library DeleteLibrary(long libraryId);
    }
}
