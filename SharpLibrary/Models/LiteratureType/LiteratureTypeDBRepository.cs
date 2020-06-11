using System.Linq;

namespace SharpLibrary.Models
{
    public class LiteratureTypeDBRepository : ILiteratureTypeRepository
    {
        private ApplicationDBContext _context;

        public LiteratureTypeDBRepository(ApplicationDBContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<LiteratureType> LiteratureTypes => _context.LiteratureTypes;

        public LiteratureType DeleteLiteratureType(long typeId)
        {
            LiteratureType dbEntry = _context.LiteratureTypes.FirstOrDefault(elm => elm.Id == typeId);
            if (dbEntry != null)
            {
                _context.LiteratureTypes.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveLiteratureType(LiteratureType type)
        {
            if (type.Id == 0)
            {
                _context.LiteratureTypes.Add(type);
            }
            else
            {
                LiteratureType dbEntry = _context.LiteratureTypes.FirstOrDefault(elm => elm.Id == type.Id);
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
