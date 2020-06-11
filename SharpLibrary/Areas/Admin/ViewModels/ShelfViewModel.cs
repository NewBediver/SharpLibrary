using SharpLibrary.Models;
using System.Collections.Generic;

namespace SharpLibrary.Areas.Admin.ViewModels
{
    public class ShelfViewModel
    {
        public Shelf Shelf { get; set; }
        public IEnumerable<Rack> Racks { get; set; }
    }
}
