using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharpLibrary.Models
{
    public class Status
    {
        public long Id { get; set; }

        [DisplayName("Название")]
        [Required(ErrorMessage = "Пожалуйста введите название статуса")]
        public string Name { get; set; }

        [DisplayName("Описание")]
        [Required(ErrorMessage = "Пожулйста введите описание статуса")]
        public string Description { get; set; }

        public ICollection<Literature> Literatures { get; set; }

        public Status()
        {
            Literatures = new List<Literature>();
        }
    }
}
