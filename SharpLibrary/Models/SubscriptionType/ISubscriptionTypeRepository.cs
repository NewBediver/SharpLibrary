using System.Linq;

namespace SharpLibrary.Models
{
    public interface ISubscriptionTypeRepository
    {
        IQueryable<SubscriptionType> SubscriptionTypes { get; }
        void SaveSubscriptionType(SubscriptionType type);
        SubscriptionType DeleteSubscriptionType(long typeId);
    }
}
