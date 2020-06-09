using System.Collections.Generic;

namespace SharpLibrary.Models
{
    public class Publishing
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<PublishingLiterature> PublishingLiteratures { get; set; }

        public Publishing()
        {
            PublishingLiteratures = new List<PublishingLiterature>();
        }
    }
}
