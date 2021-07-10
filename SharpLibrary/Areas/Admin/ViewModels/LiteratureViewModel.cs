using SharpLibrary.Models;
using System.Collections.Generic;

namespace SharpLibrary.Areas.Admin.ViewModels
{
    public class LiteratureViewModel
    {
        public Literature Literature { get; set; }
        public IEnumerable<Status> Statuses { get; set; }
        public IEnumerable<LiteratureType> Types { get; set; }
        public IEnumerable<Shelf> Shelves { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Publishing> Publishings { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}
