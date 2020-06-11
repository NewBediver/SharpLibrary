using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharpLibrary.Models
{
    public class Publishing
    {
        public long Id { get; set; }
        
        [DisplayName("Название")]
        [Required(ErrorMessage = "Пожалуйста введите название издательства")]
        public string Name { get; set; }

        [DisplayName("Описание")]
        [Required(ErrorMessage = "Пожалуйста введите описание издательства")]        
        public string Description { get; set; }

        public ICollection<PublishingLiterature> PublishingLiteratures { get; set; }

        public Publishing()
        {
            PublishingLiteratures = new List<PublishingLiterature>();
        }
    }
}
