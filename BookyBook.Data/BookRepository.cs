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
        var books = _context.Books.ToList();
        if (books is null) {
            throw new InvalidOperationException("Error al intentar obtener los libros.");
        }
        return books;
    }

    public IEnumerable<Borrowing> GetBorrowingsByBookId(int bookId)
    {
        var book = _context.Books
            .Include(bk => bk.Borrowings) // Incluir prestamos relacionados, pero ojo con referencia circular ;-)
            .FirstOrDefault(bk => bk.IdNumber == bookId);

        if (book is null) {
            throw new KeyNotFoundException("Libro no encontrado.");
        } else if (book.Borrowings is null) {
            throw new KeyNotFoundException("No se encontraron préstamos.");
        }
        return book.Borrowings;
    }

    public Book GetBook(int bookId)
    {
        var book = _context.Books.FirstOrDefault(bk => bk.IdNumber == bookId);
        if (book is null) {
            throw new KeyNotFoundException("No se ha encontrado el libro " + bookId);
        }
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
            throw new KeyNotFoundException("Libro no encontrado.");
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
