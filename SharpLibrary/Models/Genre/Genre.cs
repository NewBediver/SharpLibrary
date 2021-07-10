using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharpLibrary.Models
{
    public class Genre
    {
        public long Id { get; set; }

        [DisplayName("Название")]
        [Required(ErrorMessage = "Пожалуйста введите название жанра")]
        public string Name { get; set; }

        [DisplayName("Описание")]
        [Required(ErrorMessage = "Пожалуйста введите описание жанра")]
        public string Description { get; set; }


        public ICollection<GenreLiterature> GenreLiteratures { get; set; }

        public Genre()
        {
            GenreLiteratures = new List<GenreLiterature>();
        }
    }
}
