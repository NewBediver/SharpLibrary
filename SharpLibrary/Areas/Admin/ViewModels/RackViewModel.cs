using SharpLibrary.Models;
using System.Collections.Generic;

namespace SharpLibrary.Areas.Admin.ViewModels
{
    public class RackViewModel
    {
        public Rack Rack { get; set; }
        public IEnumerable<Library> Libraries { get; set; }
    }
}
