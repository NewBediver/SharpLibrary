using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharpLibrary.Models
{
    public class User
    {
        public long Id { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Пожалуйста введите имя")]
        public string Name { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Пожалуйста введите фамилию")]
        public string Surname { get; set; }

        [DisplayName("Отчество")]
        [Required(ErrorMessage = "Пожалуйста введите отчество")]
        public string Patronymic { get; set; }

        [DisplayName("Дата рождения")]
        [Required(ErrorMessage = "Пожалуйста введите дату рождения")]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Роль")]
        [Required(ErrorMessage = "Пожалуйста выберите роль")]
        public long RoleId { get; set; }
        public Role Role { get; set; }

        public Subscription Subscription { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public User()
        {
            Transactions = new List<Transaction>();
        }

    }
}
