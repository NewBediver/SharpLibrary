using System.Linq;

namespace SharpLibrary.Models
{
    public class RoleDBRepository : IRoleRepository
    {
        private ApplicationDBContext _context;

        public RoleDBRepository(ApplicationDBContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Role> Roles => _context.Roles;

        public Role DeleteRole(long roleId)
        {
            Role dbEntry = _context.Roles.FirstOrDefault(elm => elm.Id == roleId);
            if (dbEntry != null)
            {
                _context.Roles.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveRole(Role role)
        {
            if (role.Id == 0)
            {
                _context.Roles.Add(role);
            }
            else
            {
                Role dbEntry = _context.Roles.FirstOrDefault(elm => elm.Id == role.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = role.Name;
                    dbEntry.Description = role.Description;
                }
            }
            _context.SaveChanges();
        }
    }
}
