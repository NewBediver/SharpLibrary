using System.Collections.Generic;

namespace SharpLibrary.Models
{
    public class Shelf
    {
        public long Id { get; set; }
        public string Number { get; set; }

        public long RackId { get; set; }
        public virtual Rack Rack { get; set; }

        public ICollection<Literature> Literatures { get; set; }

        public Shelf()
        {
            Literatures = new List<Literature>();
        }
    }
}
