﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharpLibrary.Models
{
    public class LiteratureType
    {
        public long Id { get; set; }

        [DisplayName("Название")]
        [Required(ErrorMessage = "Пожалуйста введите название литературного типа")]
        public string Name { get; set; }

        [DisplayName("Описание")]
        [Required(ErrorMessage = "Пожалуйста введите описание литературного типа")]
        public string Description { get; set; }

        public ICollection<Literature> Literatures { get; set; }

        public LiteratureType()
        {
            Literatures = new List<Literature>();
        }
    }
}
