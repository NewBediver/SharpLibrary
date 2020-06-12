using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpLibrary.Models
{
    public class AuthorDBRepository : IAuthorRepository
    {
        private ApplicationDBContext _context;

        public AuthorDBRepository(ApplicationDBContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Author> Authors => _context.Authors;

        public void SaveAuthor(Author author)
        {
            if (author.Id == 0)
            {
                _context.Authors.Add(author);
            }
            else
            {
                Author dbEntry = _context.Authors.FirstOrDefault(elm => elm.Id == author.Id);
                if (dbEntry != null)
                {
                    dbEntry.Surname = author.Surname;
                    dbEntry.Name = author.Name;
                    dbEntry.Patronymic = author.Patronymic;
                    dbEntry.Description = author.Description;
                }
            }
            _context.SaveChanges();
        }

        public Author DeleteAuthor(long authorId)
        {
            Author dbEntry = _context.Authors.FirstOrDefault(elm => elm.Id == authorId);
            if (dbEntry != null)
            {
                _context.Authors.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
