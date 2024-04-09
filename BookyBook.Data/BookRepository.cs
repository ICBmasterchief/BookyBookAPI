using BookyBook.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Runtime.InteropServices;

namespace BookyBook.Data;
public class BookRepository : IBookRepository
{
    private readonly BookyBookContext _context;
    public BookRepository(BookyBookContext context)
    {
        _context = context;
    }

    public void AddBook(Book book)
    {
        _context.Books.Add(book);
    }

    public IEnumerable<Book> GetAllBooks(BookQueryParameters? bookQueryParameters)
    {

        var query = _context.Books.AsQueryable();

        if (!string.IsNullOrWhiteSpace(bookQueryParameters.Title))
        {
            query = query.Where(bk => bk.Title.Contains(bookQueryParameters.Title));
        }

        if (!string.IsNullOrWhiteSpace(bookQueryParameters.Author))
        {
            query = query.Where(bk => bk.Author.Contains(bookQueryParameters.Author));
        }

        if (!string.IsNullOrWhiteSpace(bookQueryParameters.Genre))
        {
            query = query.Where(bk => bk.Genre.Contains(bookQueryParameters.Genre));
        }

        if (bookQueryParameters.fromYear.HasValue && bookQueryParameters.toYear.HasValue)
        {
            query = query.Where(bk => bk.Year >= bookQueryParameters.fromYear.Value 
                                    && bk.Year <= bookQueryParameters.toYear.Value);
        }
        else if (bookQueryParameters.fromYear.HasValue)
        {
            query = query.Where(bk => bk.Year >= bookQueryParameters.fromYear.Value);
        }
        else if (bookQueryParameters.toYear.HasValue)
        {
            query = query.Where(bk => bk.Year <= bookQueryParameters.toYear.Value);
        }

        var result = query.ToList();

        return result;
    }

    public IEnumerable<Borrowing> GetBorrowingsByBookId(int bookId, BookQueryParameters? bookQueryParameters)
    {
        var book = _context.Books
            .Include(bk => bk.Borrowings) // Incluir prestamos relacionados, pero ojo con referencia circular ;-)
            .FirstOrDefault(bk => bk.IdNumber == bookId);

        if (book is null) {
            throw new KeyNotFoundException("User not found.");
        }

        var query = book?.Borrowings.AsQueryable();

        if (bookQueryParameters.fromYear.HasValue)
        {
            query = query.Where(t => t.BorrowingDate.Value.Year >= bookQueryParameters.fromYear.Value);
        }

        if (bookQueryParameters.toYear.HasValue)
        {
            query = query.Where(t => t.BorrowingDate.Value.Year <= bookQueryParameters.toYear.Value);
        }

        var result = query.ToList();

        return result;
    }

    public Book GetBook(int bookId)
    {
        var book = _context.Books.FirstOrDefault(bk => bk.IdNumber == bookId);
        return book;
    }


    public void UpdateBook(Book book)
    {
        // En EF Core, si el objeto ya está siendo rastreado, actualizar sus propiedades
        // y llamar a SaveChanges() es suficiente para actualizarlo en la base de datos.
        // Asegúrate de que el estado del objeto sea 'Modified' si es necesario.
        _context.Entry(book).State = EntityState.Modified;
    }

    public void DeleteBook(int bookId) 
    {
        var book = GetBook(bookId);
        if (book is null) {
            throw new KeyNotFoundException("Book not found.");
        }
        _context.Books.Remove(book);
        SaveChanges();
    }
    
    public void SaveChanges()
    {
        _context.SaveChanges();
    }



    // public List<Book>? BooksList = new();
    // private readonly string BookJsonPath;
    // public BookRepository()
    // {
    //     if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    //     {
    //         BookJsonPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "BookyBook.Data", "Data.Books.json");
    //     } else {
    //         BookJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "BookyBook.Data", "Data.Books.json");
    //     }
    //     GetRegisteredBooks();
    // }
    // public void AddBook(Book book)
    // {
    //     BooksList.Add(book);
    //     SaveBookData();
    // }
    // public void GetRegisteredBooks()
    // {
    //     try
    //     {
    //     string JsonBooks = File.ReadAllText(BookJsonPath);
    //     BooksList =  JsonSerializer.Deserialize<List<Book>>(JsonBooks);
    //     } catch (System.Exception)
    //     {
    //         Console.WriteLine("ERROR TRYING ACCESS DATA");
    //     }
    // }
    // public void SaveBookData()
    // {
    //     string JsonBooks = JsonSerializer.Serialize(BooksList, new JsonSerializerOptions {WriteIndented = true});
    //     File.WriteAllText(BookJsonPath, JsonBooks);
    // }
}
