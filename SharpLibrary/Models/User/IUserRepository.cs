using System.Linq;

namespace SharpLibrary.Models
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        void SaveUser(User user);
        User DeleteUser(long userId);
    }
}
