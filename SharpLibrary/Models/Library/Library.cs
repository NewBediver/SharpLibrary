using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharpLibrary.Models
{
    public class Library
    {
        public long Id { get; set; }

        [DisplayName("Название")]
        [Required(ErrorMessage = "Пожалуйста введите название библиотеки")]
        public string Name { get; set; }

        [DisplayName("Описание")]
        [Required(ErrorMessage = "Пожалуйста введите описание библиотеки")]
        public string Description { get; set; }

        public ICollection<Rack> Racks { get; set; }

        public Library()
        {
            Racks = new List<Rack>();
        }
    }
}
