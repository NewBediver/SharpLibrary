using SharpLibrary.Models;
using System.Collections.Generic;

namespace SharpLibrary.Areas.Admin.ViewModels
{
    public class TransactionViewModel
    {
        public Transaction Transaction { get; set; }
        public IEnumerable<Subscription> Subscriptions { get; set; }
        public IEnumerable<TransactionType> Types { get; set; }
        public IEnumerable<Literature> Literatures { get; set; }
    }
}
