using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpLibrary.Models
{
    public class StatusDBRepository : IStatusRepository
    {
        private ApplicationDBContext _context;

        public StatusDBRepository(ApplicationDBContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Status> Statuses => _context.Statuses;

        public Status DeleteStatus(long statusId)
        {
            Status dbEntry = _context.Statuses.FirstOrDefault(elm => elm.Id == statusId);
            if (dbEntry != null)
            {
                _context.Statuses.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveStatus(Status status)
        {
            if (status.Id == 0)
            {
                _context.Statuses.Add(status);
            }
            else
            {
                Status dbEntry = _context.Statuses.FirstOrDefault(elm => elm.Id == status.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = status.Name;
                    dbEntry.Description = status.Description;
                }
            }
            _context.SaveChanges();
        }
    }
}
