using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SharpLibrary.Models
{
    public class TransactionDBRepository : ITransactionRepository
    {
        private ApplicationDBContext _context;

        public TransactionDBRepository(ApplicationDBContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Transaction> Transactions => _context.Transactions;

        public Transaction DeleteTransaction(long transactionId)
        {
            Transaction dbEntry = _context.Transactions.FirstOrDefault(elm => elm.Id == transactionId);
            if (dbEntry != null)
            {
                _context.Transactions.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveTransaction(Transaction transaction, int[] literatures)
        {
            if (transaction.Id == 0)
            {
                _context.Transactions.Add(transaction);
                foreach (var element in literatures)
                {
                    transaction.TransactionLiteratures.Add(new TransactionLiterature() { TransactionId = transaction.Id, LiteratureId = element });
                    _context.Literatures
                        .Include(elm => elm.Status)
                        .First(elm => elm.Id == element).StatusId = 3;
                }
            }
            else
            {
                Transaction dbEntry = _context.Transactions
                    .Include(elm => elm.TransactionLiteratures)
                    .FirstOrDefault(elm => elm.Id == transaction.Id);
                if (dbEntry != null)
                {
                    dbEntry.StartDate = transaction.StartDate;
                    dbEntry.EndDate = transaction.EndDate;
                    dbEntry.Description = transaction.Description;
                    dbEntry.TransactionTypeId = transaction.TransactionTypeId;
                    dbEntry.SubscriptionId = transaction.SubscriptionId;
                    foreach (var element in dbEntry.TransactionLiteratures.Select(elm => elm.Literature))
                    {
                        _context.Literatures
                            .Include(elm => elm.Status)
                            .First(elm => elm.Id == element.Id).StatusId = 1;
                    }
                    dbEntry.TransactionLiteratures.Clear();
                    foreach (var element in literatures)
                    {
                        dbEntry.TransactionLiteratures.Add(new TransactionLiterature() { TransactionId = dbEntry.Id, LiteratureId = element });
                        _context.Literatures
                            .Include(elm => elm.Status)
                            .First(elm => elm.Id == element).StatusId = 3;
                    }
                }
            }
            _context.SaveChanges();
        }
    }
}
