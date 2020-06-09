namespace SharpLibrary.Models
{
    public class TransactionLiterature
    {
        public long TransactionId { get; set; }
        public Transaction Transaction { get; set; }

        public long LiteratureId { get; set; }
        public Literature Literature { get; set; }
    }
}
