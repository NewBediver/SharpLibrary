using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpLibrary.Models
{
    public class TransactionTypeDBRepository : ITransactionTypeRepository
    {
        private ApplicationDBContext _context;

        public TransactionTypeDBRepository(ApplicationDBContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<TransactionType> TransactionTypes => _context.TransactionTypes;

        public TransactionType DeleteTransactionType(long typeId)
        {
            TransactionType dbEntry = _context.TransactionTypes.FirstOrDefault(elm => elm.Id == typeId);
            if (dbEntry != null)
            {
                _context.TransactionTypes.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveTransactionType(TransactionType type)
        {
            if (type.Id == 0)
            {
                _context.TransactionTypes.Add(type);
            }
            else
            {
                TransactionType dbEntry = _context.TransactionTypes.FirstOrDefault(elm => elm.Id == type.Id);
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
