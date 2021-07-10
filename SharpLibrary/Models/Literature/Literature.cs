using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharpLibrary.Models
{
    public class Literature
    {
        public long Id { get; set; }

        [DisplayName("Инвентарный номер")]
        [Required(ErrorMessage = "Пожалуйста введите инвентарный номер литературы")]
        public string InventoryNumber { get; set; }

        [DisplayName("Название")]
        [Required(ErrorMessage = "Пожалуйста введите название литературы")]
        public string Name { get; set; }

        [DisplayName("Дата написания")]
        [Required(ErrorMessage = "Пожалуйста введите дату написания литературы")]
        public DateTime Date { get; set; }

        [DisplayName("Количество страниц")]
        [Required(ErrorMessage = "Пожалуйста введите количество страниц")]
        [Range(1, 1000000)]
        public int Pages { get; set; }

        [DisplayName("Краткое описание")]
        [Required(ErrorMessage = "Пожалуйста введите краткое описание литературы")]
        public string ShortDescription { get; set; }

        [DisplayName("Описание")]
        [Required(ErrorMessage = "Пожалуйста введите описание литературы")]
        public string LongDescription { get; set; }

        [DisplayName("Дата получения")]
        [Required(ErrorMessage = "Пожалуйста введите дату получения литературы")]
        public DateTime ReceiptDate { get; set; }

        [DisplayName("Дата списания")]
        [Required(ErrorMessage = "Пожалуйста введите дату списания литературы")]
        public DateTime DebitDate { get; set; }


        [DisplayName("Тип литературы")]
        [Required(ErrorMessage = "Пожалуйста выберите тип литературы")]
        public long LiteratureTypeId { get; set; }
        public LiteratureType Type { get; set; }

        [DisplayName("Полка хранения")]
        [Required(ErrorMessage = "Пожалуйста выберите полку хранения литературы")]
        public long ShelfId { get; set; }
        public Shelf Shelf { get; set; }

        [DisplayName("Статус")]
        [Required(ErrorMessage = "Пожалуйста выберите статус литературы")]
        public long StatusId { get; set; }
        public Status Status { get; set; }

        public ICollection<GenreLiterature> GenreLiteratures { get; set; }
        public ICollection<PublishingLiterature> PublishingLiteratures { get; set; }
        public ICollection<AuthorLiterature> AuthorLiteratures { get; set; }
        public ICollection<TransactionLiterature> TransactionLiteratures { get; set; }

        public Literature()
        {
            GenreLiteratures = new List<GenreLiterature>();
            PublishingLiteratures = new List<PublishingLiterature>();
            AuthorLiteratures = new List<AuthorLiterature>();
            TransactionLiteratures = new List<TransactionLiterature>();
        }
    }
}
