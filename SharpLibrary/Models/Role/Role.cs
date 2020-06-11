using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharpLibrary.Models
{
    public class Role
    {
        public long Id { get; set; }

        [DisplayName("Название")]
        [Required(ErrorMessage = "Пожалуйста введите название роли")]
        public string Name { get; set; }

        [DisplayName("Описание")]
        [Required(ErrorMessage = "Пожалуйста введите описание роли")]
        public string Description { get; set; }

        public ICollection<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }
    }
}
