using Microsoft.EntityFrameworkCore;
using LibraryApi.Data;
using LibraryApi.Repositories.Interfaces;
using LibraryApi.Repositories.Implementations;
using LibraryApi.Services.Interfaces;
using LibraryApi.Services;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Репозитории
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IGenreRepository, GenreRepository>();

//Сервисы
builder.Services.AddScoped<IGenreService, GenreService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
