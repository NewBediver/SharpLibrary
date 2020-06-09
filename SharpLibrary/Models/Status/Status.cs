

using System.Collections.Generic;

namespace SharpLibrary.Models
{
    public class Status
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Literature> Literatures { get; set; }

        public Status()
        {
            Literatures = new List<Literature>();
        }
    }
}
