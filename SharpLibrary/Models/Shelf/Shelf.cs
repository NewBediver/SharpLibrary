using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharpLibrary.Models
{
    public class Shelf
    {
        public long Id { get; set; }

        [DisplayName("Номер")]
        [Required(ErrorMessage = "Пожалуйста введите номер полки")]
        public string Number { get; set; }

        [DisplayName("Библиотека")]
        [Required(ErrorMessage = "Пожалуйста Выберите библиотеку")]
        public long RackId { get; set; }
        public Rack Rack { get; set; }

        public ICollection<Literature> Literatures { get; set; }

        public Shelf()
        {
            Literatures = new List<Literature>();
        }
    }
}
