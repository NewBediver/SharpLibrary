using System.Linq;

namespace SharpLibrary.Models
{
    public interface ILiteratureTypeRepository
    {
        IQueryable<LiteratureType> LiteratureTypes { get; }
        void SaveLiteratureType(LiteratureType type);
        LiteratureType DeleteLiteratureType(long typeId);
    }
}
