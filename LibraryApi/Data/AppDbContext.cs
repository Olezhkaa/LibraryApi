using Microsoft.EntityFrameworkCore;
using LibraryApi.Models;

namespace LibraryApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User>? Users { get; set; }
        public DbSet<Collection>? Collections { get; set; }
        public DbSet<FavoriteBook>? FavouriteBooks { get; set; }
        public DbSet<Book>? Books { get; set; }
        public DbSet<Genre>? Genres { get; set; }
        public DbSet<CollectionBook>? CollectionBooks { get; set; }
        public DbSet<Author>? Authors { get; set; }
        public DbSet<AuthorImage>? AuthorImages { get; set; }
        public DbSet<BookImage>? BookImages { get; set; }
        public DbSet<UserImage>? UserImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entiryType in modelBuilder.Model.GetEntityTypes()
            .Where(e => e.ClrType.IsSubclassOf(typeof(BaseModel)))) 
            {
                modelBuilder.Entity(entiryType.ClrType)
                .Property(nameof(BaseModel.CreatedAt))
                .HasDefaultValueSql("NOW()");

                modelBuilder.Entity(entiryType.ClrType)
                .Property(nameof(BaseModel.UpdatedAt))
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAddOrUpdate();
            }
        }
    }
}