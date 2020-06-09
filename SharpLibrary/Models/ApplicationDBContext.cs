using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SharpLibrary.Models
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Literature> Literatures { get; set; }
        public DbSet<LiteratureType> LiteratureTypes { get; set; }
        public DbSet<Publishing> Publishings { get; set; }
        public DbSet<Rack> Racks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ManyToManyAuthorLiterature(modelBuilder);
            ManyToManyGenreLiterature(modelBuilder);
            ManyToManyPublishingLiterature(modelBuilder);
            ManyToManyTransactionLiterature(modelBuilder);
        }

        private void ManyToManyAuthorLiterature(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorLiterature>()
                .HasKey(t => new { t.AuthorId, t.LiteratureId });
            modelBuilder.Entity<AuthorLiterature>()
                .HasOne(elm => elm.Author)
                .WithMany(elm => elm.AuthorLiteratures)
                .HasForeignKey(elm => elm.AuthorId);
            modelBuilder.Entity<AuthorLiterature>()
                .HasOne(al => al.Literature)
                .WithMany(l => l.AuthorLiteratures)
                .HasForeignKey(al => al.LiteratureId);
        }

        private void ManyToManyGenreLiterature(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenreLiterature>()
                .HasKey(t => new { t.GenreId, t.LiteratureId });
            modelBuilder.Entity<GenreLiterature>()
                .HasOne(elm => elm.Genre)
                .WithMany(elm => elm.GenreLiteratures)
                .HasForeignKey(elm => elm.GenreId);
            modelBuilder.Entity<GenreLiterature>()
                .HasOne(elm => elm.Literature)
                .WithMany(elm => elm.GenreLiteratures)
                .HasForeignKey(elm => elm.LiteratureId);
        }

        private void ManyToManyPublishingLiterature(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PublishingLiterature>()
                .HasKey(t => new { t.PublishingId, t.LiteratureId });
            modelBuilder.Entity<PublishingLiterature>()
                .HasOne(elm => elm.Publishing)
                .WithMany(elm => elm.PublishingLiteratures)
                .HasForeignKey(elm => elm.PublishingId);
            modelBuilder.Entity<PublishingLiterature>()
                .HasOne(elm => elm.Literature)
                .WithMany(elm => elm.PublishingLiteratures)
                .HasForeignKey(elm => elm.LiteratureId);
        }

        private void ManyToManyTransactionLiterature(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionLiterature>()
                .HasKey(t => new { t.TransactionId, t.LiteratureId });
            modelBuilder.Entity<TransactionLiterature>()
                .HasOne(elm => elm.Transaction)
                .WithMany(elm => elm.TransactionLiteratures)
                .HasForeignKey(elm => elm.TransactionId);
            modelBuilder.Entity<TransactionLiterature>()
                .HasOne(elm => elm.Literature)
                .WithMany(elm => elm.TransactionLiteratures)
                .HasForeignKey(elm => elm.LiteratureId);
        }
    }
}
