using Microsoft.EntityFrameworkCore;
using LibraryApi.Models;

namespace LibraryApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // public DbSet<User>? Users { get; set; }
        // public DbSet<Collection>? Collections { get; set; }
        // public DbSet<FavouriteBook>? FavouriteBooks { get; set; }
        // public DbSet<Book>? Books { get; set; }
        // public DbSet<Genre>? Genres { get; set; }
        // public DbSet<CollectionBook>? CollectionBooks { get; set; }
    }
}