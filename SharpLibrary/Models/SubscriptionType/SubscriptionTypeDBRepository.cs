using System.Linq;

namespace SharpLibrary.Models
{
    public class SubscriptionTypeDBRepository : ISubscriptionTypeRepository
    {
        private ApplicationDBContext _context;

        public SubscriptionTypeDBRepository(ApplicationDBContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<SubscriptionType> SubscriptionTypes => _context.SubscriptionTypes;

        public SubscriptionType DeleteSubscriptionType(long typeId)
        {
            SubscriptionType dbEntry = _context.SubscriptionTypes.FirstOrDefault(elm => elm.Id == typeId);
            if (dbEntry != null)
            {
                _context.SubscriptionTypes.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveSubscriptionType(SubscriptionType type)
        {
            if (type.Id == 0)
            {
                _context.SubscriptionTypes.Add(type);
            }
            else
            {
                SubscriptionType dbEntry = _context.SubscriptionTypes.FirstOrDefault(elm => elm.Id == type.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = type.Name;
                    dbEntry.Description = type.Description;
                }
            }
            _context.SaveChanges();
        }
    }
}
