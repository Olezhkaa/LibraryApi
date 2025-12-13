using Microsoft.EntityFrameworkCore;
using LibraryApi.Data;
using LibraryApi.Repositories.Interfaces;
using LibraryApi.Repositories.Implementations;
using LibraryApi.Services.Interfaces;
using LibraryApi.Services;
using LibraryApi.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Разрешаем ВСЕ запросы (для разработки)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()    // Разрешить любой домен
                   .AllowAnyMethod()    // Разрешить любые методы (GET, POST и т.д.)
                   .AllowAnyHeader();   // Разрешить любые заголовки
        });
});

// PostgreSQL connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Репозитории
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICollectionRepository, CollectionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICollectionBookRepository, CollectionBookRepository>();
builder.Services.AddScoped<IFavoriteBookRepository, FavoriteBookRepository>();
builder.Services.AddScoped<IAuthorImageRepository, AuthorImageRepository>();
builder.Services.AddScoped<IBookImageRepository, BookImageRepository>();
builder.Services.AddScoped<IUserImageRepository, UserImageRepository>();

//Сервисы
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICollectionBookService, CollectionBookService>();
builder.Services.AddScoped<IFavoriteBookService, FavoriteBookService>();
builder.Services.AddScoped<IAuthorImageService, AuthorImageService>();
builder.Services.AddScoped<IBookImageService, BookImageService>();
builder.Services.AddScoped<IUserImageService, UserImageService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Подключаем CORS
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run("http://0.0.0.0:5050");
