using System.Linq;

namespace SharpLibrary.Models
{
    public interface IPublishingRepository
    {
        IQueryable<Publishing> Publishings { get; }
        void SavePublishing(Publishing publishing);
        Publishing DeletePublishing(long publishingId);
    }
}
