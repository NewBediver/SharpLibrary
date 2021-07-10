using System;
using System.Linq;

namespace SharpLibrary.Models
{
    public class SubscriptionDBRepository : ISubscriptionRepository
    {
        private ApplicationDBContext _context;

        public SubscriptionDBRepository(ApplicationDBContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Subscription> Subscriptions => _context.Subscriptions;

        public Subscription DeleteSubscription(long subscriptionId)
        {
            Subscription dbEntry = _context.Subscriptions.FirstOrDefault(elm => elm.Id == subscriptionId);
            if (dbEntry != null)
            {
                _context.Subscriptions.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveSubscription(Subscription subscription)
        {
            if (subscription.Id == 0)
            {
                _context.Subscriptions.Add(subscription);
            }
            else
            {
                Subscription dbEntry = _context.Subscriptions.FirstOrDefault(elm => elm.Id == subscription.Id);
                if (dbEntry != null)
                {
                    dbEntry.DateOfStart = subscription.DateOfStart;
                    dbEntry.SubscriptionTypeId = subscription.SubscriptionTypeId;
                }
            }
            _context.SaveChanges();
        }
    }
}
