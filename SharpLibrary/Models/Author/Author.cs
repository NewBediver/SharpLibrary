using System.Collections.Generic;

namespace SharpLibrary.Models
{
    public class Author
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Literature> Literatures { get; set; }

        public Author()
        {
            Literatures = new List<Literature>();
        }
    }
}
