using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharpLibrary.Models
{
    public class Author
    {
        public long Id { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Пожалуйста введите имя автора")]
        public string Name { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Пожалуйста введите фамилию автора")]
        public string Surname { get; set; }

        [DisplayName("Отчество")]
        [Required(ErrorMessage = "Пожалуйста введите отчество автора")]
        public string Patronymic { get; set; }

        [DisplayName("Описание")]
        [Required(ErrorMessage = "Пожалуйста введите описание")]
        public string Description { get; set; }

        public ICollection<AuthorLiterature> AuthorLiteratures { get; set; }

        public Author()
        {
            AuthorLiteratures = new List<AuthorLiterature>();
        }
    }
}
