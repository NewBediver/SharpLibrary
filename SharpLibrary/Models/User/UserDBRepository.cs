using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpLibrary.Models
{
    public class UserDBRepository : IUserRepository
    {
        private ApplicationDBContext _context;

        public UserDBRepository(ApplicationDBContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<User> Users => _context.Users;

        public User DeleteUser(long userId)
        {
            User dbEntry = _context.Users.FirstOrDefault(elm => elm.Id == userId);
            if (dbEntry != null)
            {
                _context.Users.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveUser(User user)
        {
            if (user.Id == 0)
            {
                _context.Users.Add(user);
            }
            else
            {
                User dbEntry = _context.Users.FirstOrDefault(elm => elm.Id == user.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = user.Name;
                    dbEntry.Subscription = user.Subscription;
                    dbEntry.Patronymic = user.Patronymic;
                    dbEntry.DateOfBirth = user.DateOfBirth;
                    dbEntry.RoleId = user.RoleId;
                }
            }
            _context.SaveChanges();
        }
    }
}
