using System.Linq;

namespace SharpLibrary.Models
{
    public interface IRackRepository
    {
        IQueryable<Rack> Racks { get; }
        void SaveRack(Rack rack);
        Rack DeleteRack(long rackId);
    }
}
