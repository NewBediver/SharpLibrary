using System.Collections.Generic;

namespace SharpLibrary.Models
{
    public class Rack
    {
        public long Id { get; set; }
        public string Number { get; set; }
        
        public long LibraryId { get; set; }
        public virtual Library Library { get; set; }

        public virtual ICollection<Shelf> Shelves { get; set; }

        public Rack()
        {
            Shelves = new List<Shelf>();
        }
    }
}
