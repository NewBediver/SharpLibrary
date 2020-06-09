using System.Collections.Generic;

namespace SharpLibrary.Models
{
    public class LiteratureType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Literature> Literatures { get; set; }

        public LiteratureType()
        {
            Literatures = new List<Literature>();
        }
    }
}
