using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpLibrary.Models
{
    public class RackDBRepository : IRackRepository
    {
        private ApplicationDBContext _context;

        public RackDBRepository(ApplicationDBContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Rack> Racks => _context.Racks;

        public Rack DeleteRack(long rackId)
        {
            Rack dbEntry = _context.Racks.FirstOrDefault(elm => elm.Id == rackId);
            if (dbEntry != null)
            {
                _context.Racks.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveRack(Rack rack)
        {
            if (rack.Id == 0)
            {
                _context.Racks.Add(rack);
            }
            else
            {
                Rack dbEntry = _context.Racks.FirstOrDefault(elm => elm.Id == rack.Id);
                if (dbEntry != null)
                {
                    dbEntry.Number = rack.Number;
                    dbEntry.LibraryId = rack.LibraryId;
                }
            }
            _context.SaveChanges();
        }
    }
}
