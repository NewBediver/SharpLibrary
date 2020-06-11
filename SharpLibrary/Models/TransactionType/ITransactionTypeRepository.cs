using System.Linq;

namespace SharpLibrary.Models
{
    public interface ITransactionTypeRepository
    {
        IQueryable<TransactionType> TransactionTypes { get; }
        void SaveTransactionType(TransactionType type);
        TransactionType DeleteTransactionType(long typeId);
    }
}
