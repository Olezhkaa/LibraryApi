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
            //Конфигурация для всех наследников BaseModel
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

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasIndex(g => g.Title);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                //Индексация полей
                entity.HasIndex(a => new { a.FirstName, a.LastName });
                entity.HasIndex(a => a.Country);

                //Вычесление поля fullName в бд, не используя ресурсы приложения
                entity.Property(a => a.FullName)
                 .HasComputedColumnSql("\"first_name\" || ' ' || \"last_name\"", stored: true);

            });

            modelBuilder.Entity<AuthorImage>(entity =>
            {
                //Индексация
                entity.HasIndex(ai => ai.AuthorId);
                entity.HasIndex(ai => ai.IsMain);
                entity.HasIndex(ai => new { ai.AuthorId, ai.IsMain })
                      .HasFilter("\"is_main\" = true");

                entity.HasOne(ai => ai.Author)
                    .WithMany(a => a.AuthorImages)
                    .HasForeignKey(ai => ai.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Ограничения
                entity.Property(ai => ai.FileSize)
                      .HasAnnotation("Range", new[] { 1L, 10L * 1024L * 1024L }); // Макс 10MB

            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(b => b.Title);
                entity.HasIndex(b => b.AuthorId);
                entity.HasIndex(b => b.GenreId);

                entity.HasOne(b => b.Author)
                    .WithMany(a => a.Books)
                    .HasForeignKey(b => b.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.Genre)
                    .WithMany(a => a.Books)
                    .HasForeignKey(b => b.GenreId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BookImage>(entity =>
            {
                //Индексация
                entity.HasIndex(bi => bi.BookId);
                entity.HasIndex(bi => bi.IsMain);
                entity.HasIndex(bi => new { bi.BookId, bi.IsMain })
                      .HasFilter("\"is_main\" = true");

                entity.HasOne(bi => bi.Book)
                    .WithMany(b => b.BookImages)
                    .HasForeignKey(bi => bi.BookId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Ограничения
                entity.Property(bi => bi.FileSize)
                      .HasAnnotation("Range", new[] { 1L, 10L * 1024L * 1024L }); // Макс 10MB

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => new { u.LastName, u.FirstName });

                entity.Property(u => u.FullName)
                      .HasComputedColumnSql("\"first_name\" || ' ' || \"last_name\"", stored: true);
            });

            modelBuilder.Entity<UserImage>(entity =>
            {
                //Индексация
                entity.HasIndex(ui => ui.UserId);

                entity.HasOne(ui => ui.User)
                    .WithMany(u => u.UserImages)
                    .HasForeignKey(ui => ui.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Ограничения
                entity.Property(bi => bi.FileSize)
                      .HasAnnotation("Range", new[] { 1L, 10L * 1024L * 1024L }); // Макс 10MB

            });

            modelBuilder.Entity<Collection>(entity =>
            {
                entity.HasIndex(c => c.Title);
            });

            modelBuilder.Entity<CollectionBook>(entity =>
            {
                entity.HasIndex(cb => cb.UserId);
                entity.HasIndex(cb => cb.BookId);
                entity.HasIndex(cb => cb.CollectionId);

                entity.HasOne(cb => cb.User)
                    .WithMany(u => u.CollectionBooks)
                    .HasForeignKey(cb => cb.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(cb => cb.Book)
                    .WithMany(b => b.CollectionBooks)
                    .HasForeignKey(cb => cb.BookId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(cb => cb.Collection)
                    .WithMany(c => c.CollectionBooks)
                    .HasForeignKey(cb => cb.CollectionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<FavoriteBook>(entity =>
            {
                entity.HasIndex(cb => cb.UserId);
                entity.HasIndex(cb => cb.BookId);

                entity.HasOne(cb => cb.User)
                    .WithMany(u => u.FavoriteBooks)
                    .HasForeignKey(cb => cb.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(cb => cb.Book)
                    .WithMany(b => b.FavoriteBooks)
                    .HasForeignKey(cb => cb.BookId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Автоматическое обновление UpdatedAt
            var entries = ChangeTracker.Entries<BaseModel>()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}