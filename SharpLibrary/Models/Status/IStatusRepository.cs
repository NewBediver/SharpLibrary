using System.Linq;

namespace SharpLibrary.Models
{
    public interface IStatusRepository
    {
        IQueryable<Status> Statuses { get; }
        void SaveStatus(Status status);
        Status DeleteStatus(long statusId);
    }
}
