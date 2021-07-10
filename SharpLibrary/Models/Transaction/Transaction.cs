using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharpLibrary.Models
{
    public class Transaction
    {
        public long Id { get; set; }

        [DisplayName("Дата создания транзакции")]
        [Required(ErrorMessage = "Пожалуйста введите дату создания транзакции")]
        public DateTime StartDate { get; set; }

        [DisplayName("Дата закрытия транзакции")]
        [Required(ErrorMessage = "Пожалуйста введите дату закрытия транзакции")]
        public DateTime EndDate { get; set; }

        [DisplayName("Описание")]
        [Required(ErrorMessage = "Пожалуйста введите описание")]
        public string Description { get; set; }


        [DisplayName("Тип транзакции")]
        [Required(ErrorMessage = "Пожалуйста выберите тип транзакции")]
        public long TransactionTypeId { get; set; }
        public TransactionType Type { get; set; }

        [DisplayName("Читательский абонемент")]
        [Required(ErrorMessage = "Пожалуйста выберите читательский абонемент")]
        public long SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }

        [DisplayName("Литература")]
        [Required(ErrorMessage = "Пожалуйста выберите литературу")]
        public ICollection<TransactionLiterature> TransactionLiteratures { get; set; }

        public Transaction()
        {
            TransactionLiteratures = new List<TransactionLiterature>();
        }

    }
}
