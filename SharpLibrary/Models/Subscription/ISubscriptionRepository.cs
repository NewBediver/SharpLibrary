using System.Linq;

namespace SharpLibrary.Models
{
    public interface ISubscriptionRepository
    {
        IQueryable<Subscription> Subscriptions { get; }
        void SaveSubscription(Subscription subscription);
        Subscription DeleteSubscription(long subscriptionId);
    }
}
