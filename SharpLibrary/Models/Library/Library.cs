using System.Collections.Generic;

namespace SharpLibrary.Models
{
    public class Library
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Rack> Racks { get; set; }

        public Library()
        {
            Racks = new List<Rack>();
        }
    }
}
