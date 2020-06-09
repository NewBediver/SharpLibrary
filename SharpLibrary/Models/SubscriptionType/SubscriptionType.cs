using System.Collections.Generic;

namespace SharpLibrary.Models
{
    public class SubscriptionType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; }

        public SubscriptionType()
        {
            Subscriptions = new List<Subscription>();
        }
    }
}
