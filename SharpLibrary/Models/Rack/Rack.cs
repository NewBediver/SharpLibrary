using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharpLibrary.Models
{
    public class Rack
    {
        public long Id { get; set; }

        [DisplayName("Номер")]
        [Required(ErrorMessage = "Пожалуйста введите номер стеллажа")]
        public string Number { get; set; }

        [DisplayName("Библиотека")]
        [Required(ErrorMessage = "Пожалуйста Выберите библиотеку")]
        public long LibraryId { get; set; }
        public Library Library { get; set; }

        public ICollection<Shelf> Shelves { get; set; }

        public Rack()
        {
            Shelves = new List<Shelf>();
        }
    }
}
