using System;
using System.Collections.Generic;

namespace SharpLibrary.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }

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
