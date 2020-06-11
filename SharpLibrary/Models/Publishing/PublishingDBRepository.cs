using System.Linq;

namespace SharpLibrary.Models
{
    public class PublishingDBRepository : IPublishingRepository
    {
        private ApplicationDBContext _context;

        public PublishingDBRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public IQueryable<Publishing> Publishings => _context.Publishings;

        public void SavePublishing(Publishing publishing)
        {
            if (publishing.Id == 0)
            {
                _context.Publishings.Add(publishing);
            }
            else
            {
                Publishing dbEntry = _context.Publishings.FirstOrDefault(elm => elm.Id == publishing.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = publishing.Name;
                    dbEntry.Description = publishing.Description;
                }
            }
            _context.SaveChanges();
        }

        public Publishing DeletePublishing(long publishingId)
        {
            Publishing dbEntry = _context.Publishings.FirstOrDefault(elm => elm.Id == publishingId);
            if (dbEntry != null)
            {
                _context.Publishings.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
