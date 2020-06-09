using System;
using System.Collections.Generic;

namespace SharpLibrary.Models
{
    public class Subscription
    {
        public long Id { get; set; }
        public DateTime DateOfStart { get; set; }

        public long SubscriptionTypeId { get; set; }
        public virtual SubscriptionType Type { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public Subscription()
        {
            Transactions = new List<Transaction>();
        }
    }
}
