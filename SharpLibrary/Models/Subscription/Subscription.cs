using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharpLibrary.Models
{
    public class Subscription
    {
        public long Id { get; set; }

        [DisplayName("Дата получения читательского абонемента")]
        [Required(ErrorMessage = "Пожалуйста установите дату получения читательского абонемента")]
        public DateTime DateOfStart { get; set; }

        [DisplayName("Тип читательского абонемента")]
        [Required(ErrorMessage = "Пожалуйста выберите тип читательского абонемента")]
        public long SubscriptionTypeId { get; set; }
        public SubscriptionType Type { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

        public Subscription()
        {
            Users = new List<User>();
            Transactions = new List<Transaction>();
        }
    }
}
