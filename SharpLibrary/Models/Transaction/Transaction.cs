using System;
using System.Collections.Generic;

namespace SharpLibrary.Models
{
    public class Transaction
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public long TransactionTypeId { get; set; }
        public virtual TransactionType Type { get; set; }

        public long SubscriptionId { get; set; }
        public virtual Subscription Subscription { get; set; }

        public long UserId { get; set; }
        public virtual User Worker { get; set; }

        public ICollection<Literature> Literatures { get; set; }

        public Transaction()
        {
            Literatures = new List<Literature>();
        }

    }
}
