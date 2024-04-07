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
                new User { IdNumber = 1111, Name = "Ignacio", Email = "ignaciocasaus1cns@gmail.com", Password = "patata", PenaltyFee = 0, RegistrationDate = new DateTime(2024,04,04,19,0,0)},
                new User { IdNumber = 1112, Name = "Alex", Email = "emaildealex@gmail.com", Password = "pimiento", PenaltyFee = 0, RegistrationDate = new DateTime(2024,04,05,18,30,0)}
            );
            modelBuilder.Entity<Book>().HasData(
                new Book {IdNumber = 10001, Title = "the fellowship of the ring", Author = "j. r. r. tolkien", Genre = "fantasy", Year = 1954, Copies = 1, Score = (decimal)8.8},                
                new Book {IdNumber = 10002, Title = "el capitan alatriste", Author = "arturo perez reverte", Genre = "historical novel", Year = 1996, Copies = 1, Score = (decimal)7.44},
                new Book {IdNumber = 10003, Title = "frankenstein", Author = "mary shelley", Genre = "gotic novel", Year = 1818, Copies = 2, Score = (decimal)7.8},
                new Book {IdNumber = 10004, Title = "harry potter y la piedra filosofal", Author = "j. k. rowling", Genre = "fantasy", Year = 1997, Copies = 1, Score = (decimal)8.0}
            );
            modelBuilder.Entity<Borrowing>().HasData(
                new Borrowing {IdNumber = 1, UserId = 1111, BookId = 10001, BorrowingDate = new DateTime(2023,12,19,00,00,00), DateToReturn = new DateTime(2024,01,02,00,00,00), ReturnedDate = new DateTime(2024,01,01,00,00,00), Returned = true, PenaltyFee = 0},
                new Borrowing {IdNumber = 2, UserId = 1112, BookId = 10003, BorrowingDate = new DateTime(2024,03,15,00,00,00), DateToReturn = new DateTime(2024,03,30,00,00,00), ReturnedDate = new DateTime(2024,04,05,00,00,00), Returned = true, PenaltyFee = 10},
                new Borrowing {IdNumber = 3, UserId = 1111, BookId = 10004, BorrowingDate = new DateTime(2024,04,05,00,00,00), DateToReturn = new DateTime(2024,04,19,00,00,00), ReturnedDate = null, Returned = false, PenaltyFee = 0}
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
