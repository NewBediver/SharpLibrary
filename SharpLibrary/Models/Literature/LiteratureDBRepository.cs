using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace SharpLibrary.Models
{
    public class LiteratureDBRepository : ILiteratureRepository
    {
        private ApplicationDBContext _context;

        public LiteratureDBRepository(ApplicationDBContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Literature> Literatures => _context.Literatures;

        public Literature DeleteLiterature(long literatureId)
        {
            Literature dbEntry = _context.Literatures.FirstOrDefault(elm => elm.Id == literatureId);
            if (dbEntry != null)
            {
                _context.Literatures.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveLiterature(Literature literature, long[] authors, long[] genres, long[] publishings)
        {
            if (literature.Id == 0)
            {
                _context.Literatures.Add(literature);
                foreach (var element in authors)
                {
                    literature.AuthorLiteratures.Add(new AuthorLiterature { LiteratureId = literature.Id, AuthorId = element });
                }
                foreach (var element in genres)
                {
                    literature.GenreLiteratures.Add(new GenreLiterature { LiteratureId = literature.Id, GenreId = element });
                }
                foreach (var element in publishings)
                {
                    literature.PublishingLiteratures.Add(new PublishingLiterature { LiteratureId = literature.Id, PublishingId = element });
                }
            }
            else
            {
                Literature dbEntry = _context.Literatures
                    .Include(elm => elm.AuthorLiteratures)
                    .Include(elm => elm.PublishingLiteratures)
                    .Include(elm => elm.GenreLiteratures)
                    .FirstOrDefault(elm => elm.Id == literature.Id);
                if (dbEntry != null)
                {
                    dbEntry.InventoryNumber = literature.InventoryNumber;
                    dbEntry.Name = literature.Name;
                    dbEntry.Date = literature.Date;
                    dbEntry.Pages = literature.Pages;
                    dbEntry.ShortDescription = literature.ShortDescription;
                    dbEntry.LongDescription = literature.LongDescription;
                    dbEntry.ReceiptDate = literature.ReceiptDate;
                    dbEntry.DebitDate = literature.DebitDate;
                    dbEntry.LiteratureTypeId = literature.LiteratureTypeId;
                    dbEntry.ShelfId = literature.ShelfId;
                    dbEntry.StatusId = literature.StatusId;
                    dbEntry.AuthorLiteratures.Clear();
                    dbEntry.PublishingLiteratures.Clear();
                    dbEntry.GenreLiteratures.Clear();
                    foreach (var element in authors)
                    {
                        dbEntry.AuthorLiteratures.Add(new AuthorLiterature { LiteratureId = literature.Id, AuthorId = element });
                    }
                    foreach (var element in genres)
                    {
                        dbEntry.GenreLiteratures.Add(new GenreLiterature { LiteratureId = literature.Id, GenreId = element });
                    }
                    foreach (var element in publishings)
                    {
                        dbEntry.PublishingLiteratures.Add(new PublishingLiterature { LiteratureId = literature.Id, PublishingId = element });
                    }
                }
            }
            _context.SaveChanges();
        }
    }
}
