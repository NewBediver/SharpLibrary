using System;
using System.Collections.Generic;

namespace SharpLibrary.Models
{
    public class Literature
    {
        public long Id { get; set; }
        public string InventoryNumber { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Pages { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime DebitDate { get; set; }

        public long LiteratureTypeId { get; set; }
        public LiteratureType Type { get; set; }

        public long ShelfId { get; set; }
        public Shelf Shelf { get; set; }

        public long StatusId { get; set; }
        public Status Status { get; set; }

        public ICollection<GenreLiterature> GenreLiteratures { get; set; }
        public ICollection<PublishingLiterature> PublishingLiteratures { get; set; }
        public ICollection<AuthorLiterature> AuthorLiteratures { get; set; }
        public ICollection<TransactionLiterature> TransactionLiteratures { get; set; }

        public Literature()
        {
            GenreLiteratures = new List<GenreLiterature>();
            PublishingLiteratures = new List<PublishingLiterature>();
            AuthorLiteratures = new List<AuthorLiterature>();
            TransactionLiteratures = new List<TransactionLiterature>();
        }
    }
}
