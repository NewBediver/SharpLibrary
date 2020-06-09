using System.Linq;

namespace SharpLibrary.Models
{
    public interface ILiteratureRepository
    {
        IQueryable<Literature> Literature { get; }
    }
}
