using System.Linq;

namespace SharpLibrary.Models
{
    public interface ITransactionRepository
    {
        IQueryable<Transaction> Transactions { get; }
        void SaveTransaction(Transaction transaction, int[] literatures);
        Transaction DeleteTransaction(long transactionId);
    }
}
