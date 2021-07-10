using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharpLibrary.Models
{
    public class TransactionType
    {
        public long Id { get; set; }

        [DisplayName("Название")]
        [Required(ErrorMessage = "Пожалуйста введите название типа транзакции")]
        public string Name { get; set; }

        [DisplayName("Описание")]
        [Required(ErrorMessage = "Пожалуйста введите описание типа транзакции")]
        public string Description { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public TransactionType()
        {
            Transactions = new List<Transaction>();
        }
    }
}
