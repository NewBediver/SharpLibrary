using SharpLibrary.Models;
using System.Collections.Generic;

namespace SharpLibrary.Areas.Admin.ViewModels
{
    public class UserViewModel
    {
        public User User { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}
