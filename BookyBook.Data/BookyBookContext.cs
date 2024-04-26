using Microsoft.EntityFrameworkCore;
using BookyBook.Models;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Data.Common;

namespace BookyBook.Data
{
    public class BookyBookContext : DbContext
    {

        public BookyBookContext(DbContextOptions<BookyBookContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>()
                .Property(usr => usr.IdNumber)
                .HasColumnName("Id");
            modelBuilder.Entity<Book>()
                .Property(bk => bk.IdNumber)
                .HasColumnName("Id");
            modelBuilder.Entity<Borrowing>()
                .Property(brw => brw.IdNumber)
                .HasColumnName("Id");
            modelBuilder.Entity<User>().HasData(
                new User { IdNumber = 1111, Name = "Admin", Email = "admin@admin.com", Password = "admin", PenaltyFee = 0, RegistrationDate = new DateTime(2024,04,03,17,0,0), Role = Roles.Admin},
                new User { IdNumber = 1112, Name = "Ignacio", Email = "ignaciocasaus1cns@gmail.com", Password = "patata", PenaltyFee = 0, RegistrationDate = new DateTime(2024,04,04,19,0,0), Role = Roles.User},
                new User { IdNumber = 1113, Name = "Alex", Email = "emaildealex@gmail.com", Password = "pimiento", PenaltyFee = 0, RegistrationDate = new DateTime(2024,04,05,18,30,0), Role = Roles.User},
                new User { IdNumber = 1114, Name = "Pepe", Email = "pepe@pepe.com", Password = "pepe", PenaltyFee = 10, RegistrationDate = new DateTime(2024,04,15,19,30,0), Role = Roles.User}
            );
            modelBuilder.Entity<Book>().HasData(
                new Book {IdNumber = 10001, Title = "La comunidad del anillo", Author = "J. R. R. Tolkien", Genre = "Fantasia", Year = 1954, Copies = 1, Score = (decimal)8.8},                
                new Book {IdNumber = 10002, Title = "El capitan alatriste", Author = "Arturo Perez Reverte", Genre = "Novela historica", Year = 1996, Copies = 1, Score = (decimal)7.44},
                new Book {IdNumber = 10003, Title = "Frankenstein", Author = "Mary Shelley", Genre = "Novela gotica", Year = 1818, Copies = 2, Score = (decimal)7.8},
                new Book {IdNumber = 10004, Title = "Harry Potter y la piedra filosofal", Author = "J. K. Rowling", Genre = "Fantasia", Year = 1997, Copies = 1, Score = (decimal)8.0},
                new Book {IdNumber = 10005, Title = "El libro de los portales", Author = "Laura Gallego Garcia", Genre = "Fantasia", Year = 2013, Copies = 3, Score = (decimal)7.2},                
                new Book {IdNumber = 10006, Title = "El nombre del viento", Author = "Patrick Rothfuss", Genre = "Fantasia", Year = 2007, Copies = 4, Score = (decimal)8.1},
                new Book {IdNumber = 10007, Title = "El informe pelicano", Author = "John Grisham", Genre = "Thriller", Year = 1992, Copies = 2, Score = (decimal)7.7},
                new Book {IdNumber = 10008, Title = "El resplandor", Author = "Stephen King", Genre = "Terror", Year = 1977, Copies = 1, Score = (decimal)7.9}
            );
            modelBuilder.Entity<Borrowing>().HasData(
                new Borrowing {IdNumber = 1, UserId = 1111, BookId = 10001, BorrowingDate = new DateTime(2023,12,19,00,00,00), DateToReturn = new DateTime(2024,01,02,00,00,00), ReturnedDate = new DateTime(2024,01,01,00,00,00), Returned = true, PenaltyFee = 0},
                new Borrowing {IdNumber = 2, UserId = 1112, BookId = 10003, BorrowingDate = new DateTime(2024,03,15,00,00,00), DateToReturn = new DateTime(2024,03,30,00,00,00), ReturnedDate = new DateTime(2024,04,05,00,00,00), Returned = true, PenaltyFee = 10},
                new Borrowing {IdNumber = 3, UserId = 1111, BookId = 10004, BorrowingDate = new DateTime(2024,04,05,00,00,00), DateToReturn = new DateTime(2024,04,19,00,00,00), ReturnedDate = null, Returned = false, PenaltyFee = 0},
                new Borrowing {IdNumber = 4, UserId = 1113, BookId = 10005, BorrowingDate = new DateTime(2023,12,19,00,00,00), DateToReturn = new DateTime(2024,01,02,00,00,00), ReturnedDate = new DateTime(2024,01,01,00,00,00), Returned = true, PenaltyFee = 0},
                new Borrowing {IdNumber = 5, UserId = 1114, BookId = 10008, BorrowingDate = new DateTime(2024,04,01,00,00,00), DateToReturn = new DateTime(2024,04,15,00,00,00), ReturnedDate = new DateTime(2024,04,21,00,00,00), Returned = true, PenaltyFee = 10},
                new Borrowing {IdNumber = 6, UserId = 1113, BookId = 10006, BorrowingDate = new DateTime(2024,04,15,00,00,00), DateToReturn = new DateTime(2024,04,29,00,00,00), ReturnedDate = null, Returned = false, PenaltyFee = 0}
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging(); // Opcional
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
       
    }
}
