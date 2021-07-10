using System.Linq;

namespace SharpLibrary.Models
{
    public interface IRoleRepository
    {
        IQueryable<Role> Roles { get; }
        void SaveRole(Role role);
        Role DeleteRole(long roleId);
    }
}
