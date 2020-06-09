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
        public virtual LiteratureType Type { get; set; }

        public long ShelfId { get; set; }
        public virtual Shelf Shelf { get; set; }

        public long StatusId { get; set; }
        public virtual Status Status { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Publishing> Publishings { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        public Literature()
        {
            Genres = new List<Genre>();
            Publishings = new List<Publishing>();
            Authors = new List<Author>();
            Transactions = new List<Transaction>();
        }
    }
}
