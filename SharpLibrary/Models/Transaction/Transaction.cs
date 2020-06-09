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
        public TransactionType Type { get; set; }

        public long SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }

        public ICollection<TransactionLiterature> TransactionLiteratures { get; set; }

        public Transaction()
        {
            TransactionLiteratures = new List<TransactionLiterature>();
        }

    }
}
