using BookyBook.Data;
using BookyBook.Business;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBorrowingService, BorrowingService>();
builder.Services.AddScoped<IBorrowingRepository, BorrowingRepository>();

// Obteniendo la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("ServerDB_localhost");

builder.Services.AddDbContext<BookyBookContext>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) //DISABLE DUE TO CONTAINERING APP
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
