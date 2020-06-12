using System.Linq;

namespace SharpLibrary.Models
{
    public interface ILiteratureRepository
    {
        IQueryable<Literature> Literatures { get; }
        void SaveLiterature(Literature literature, long[] authors, long[] genres, long[] publishings);
        Literature DeleteLiterature(long literatureId);
    }
}
