using System.Collections.Generic;

namespace SharpLibrary.Models
{
    public class TransactionType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public TransactionType()
        {
            Transactions = new List<Transaction>();
        }
    }
}
