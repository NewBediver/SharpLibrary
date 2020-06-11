using SharpLibrary.Models;
using System.Collections.Generic;

namespace SharpLibrary.Areas.Admin.ViewModels
{
    public class SubscriptionViewModel
    {
        public Subscription Subscription { get; set; }
        public IEnumerable<SubscriptionType> Types { get; set; }
    }
}
