using BookyBook.Data;
using BookyBook.Models;
//using Spectre.Console;

namespace BookyBook.Business;
public class BookService : IBookService
{

    private readonly IBookRepository _repository;
    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }
    public IEnumerable<Book> GetAllBooks(BookQueryParameters? bookQueryParameters)
    {
        
         var query = _repository.GetAllBooks(bookQueryParameters).AsQueryable();

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
        
        var query = _repository.GetBorrowingsByBookId(bookId, bookQueryParameters).AsQueryable();

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
        var book = _repository.GetBook(bookId);
        if (book == null) throw new ArgumentNullException(nameof(book));
        return book;
    }

    public Book AddBook(BookCreateDTO bookCreate)
    {
        var book = new Book(bookCreate.Title, bookCreate.Author, bookCreate.Genre, bookCreate.Year, bookCreate.Copies, bookCreate.Score);
            _repository.AddBook(book);
            _repository.SaveChanges();
            return book;
    }

    public void UpdateBook(int bookId, BookUpdateDTO bookUpdate)
    {
        var book = _repository.GetBook(bookId);
        if (book == null)
        {
            throw new KeyNotFoundException($"Libro {bookId} no encontrado.");
        }

        book.Title = bookUpdate.Title;
        book.Author = bookUpdate.Author;
        book.Genre = bookUpdate.Genre;
        book.Year = bookUpdate.Year;
        book.Score = bookUpdate.Score;
        _repository.UpdateBook(book);
        _repository.SaveChanges();
    }

    public void DeleteBook(int bookId)
    {
        var book = _repository.GetBook(bookId);
        if (book == null)
        {
            throw new KeyNotFoundException($"Libro {bookId} no encontrado.");
        }

        _repository.DeleteBook(bookId);
    }



    // public readonly BookRepository bookData = new();
    // public Table BookTable = new();
    // private int existingBookIndex;
    // public void SearchForBooks()
    // {
    //     AnsiConsole.MarkupLine("[green]Searching for books[/]");
    //     AnsiConsole.MarkupLine("");
    //     string searchText = AnsiConsole.Ask<String>("Write book title or author:").ToLower();
    //     AnsiConsole.WriteLine("");
    //     List<Book> findedBooks =  bookData.BooksList.FindAll(x => x.Title.Contains(searchText) || x.Author.Contains(searchText));
    //     if (findedBooks.Count != 0)
    //     {
    //         CreateBookTable(findedBooks);
    //         AnsiConsole.Write(BookTable);
    //         AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices("<-Back to menu"));
    //     } else {
    //         AnsiConsole.MarkupLine("[yellow]No books found, sorry :([/]");
    //         Thread.Sleep(2000);
    //     }
    // }
    // public void ShowLibrary()
    // {
    //     List<Book> listBooks = GetBookList();
    //     if (listBooks.Count != 0)
    //     {
    //         AnsiConsole.MarkupLine("[green]Library[/]");
    //         AnsiConsole.MarkupLine("");
    //         CreateBookTable(listBooks);
    //         AnsiConsole.Write(BookTable);
    //         AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices("<-Back to menu"));
    //     } else {
    //         AnsiConsole.MarkupLine("[yellow]We don't have any books yet, sorry :([/]");
    //         Thread.Sleep(2000);
    //     }
    // }
    
    // public void DonateBook()
    // {
    //     AnsiConsole.MarkupLine("[green]Donating a book[/]");
    //     AnsiConsole.MarkupLine("");
    //     if (AnsiConsole.Confirm("Are you sure you want to donate a book?"))
    //     {
    //         AnsiConsole.MarkupLine("[yellow]What book do you want to donate?[/]");
    //         AnsiConsole.MarkupLine("");
    //         string title = AnsiConsole.Ask<String>("Book title:").ToLower();
    //         string author = AnsiConsole.Ask<String>("Author:").ToLower();
    //         int copies;
    //         if (bookData.BooksList.Count > 0 && CheckExistingBookData(title, author))
    //         {
    //             AnsiConsole.MarkupLine("[yellow]We already have this book.[/]");
    //             if (AnsiConsole.Confirm("Do you want to add new copies?"))
    //             {
    //                 copies = AnsiConsole.Ask<int>("Copies to donate:");
    //                 bookData.BooksList[existingBookIndex].Copies += copies;
    //                 bookData.SaveBookData();
    //                 AnsiConsole.MarkupLine("[yellow]Copies added to our library.[/]");
    //                 AnsiConsole.MarkupLine("Thank you!");
    //             } else {
    //                 AnsiConsole.MarkupLine("Ok... :(");
    //             }
    //         } else {
    //             string genre = AnsiConsole.Ask<String>("Genre:").ToLower();
    //             int year = AnsiConsole.Ask<int>("Year:");
    //             copies = AnsiConsole.Ask<int>("Copies to donate:");
    //             decimal score = decimal.Parse(AnsiConsole.Ask<String>("Score:"));
    //             Book book = new(title, author, genre, year, copies, score);
    //             if (bookData.BooksList.Count == 0){
    //                 bookData.AddBook(book);
    //             } else {
    //                 int num = bookData.BooksList.Last().IdNumber;
    //                 num++;
    //                 book = new(title, author, genre, year, copies, score, num);
    //                 bookData.AddBook(book);
    //             }
    //             AnsiConsole.MarkupLine("[yellow]New books added to our library.[/]");
    //             AnsiConsole.MarkupLine("Thank you!");
    //         }
    //     } else {
    //         AnsiConsole.MarkupLine("Ok... :(");
    //     }
    //     Thread.Sleep(3000);
    // }
    // public bool CheckExistingBookData(string? title, string? author)
    // {
    //     existingBookIndex = 0;
    //     foreach (var book in bookData.BooksList)
    //     {
    //         if (book.Title == title && book.Author == author)
    //         {
    //             return true;
    //         }
    //         existingBookIndex++;
    //     }
    //     return false;
    // }
    // public bool CheckExistingBookDataById(int bookId)
    // {
    //     existingBookIndex = 0;
    //     foreach (var book in bookData.BooksList)
    //     {
    //         if (book.IdNumber == bookId)
    //         {
    //             return true;
    //         }
    //         existingBookIndex++;
    //     }
    //     return false;
    // }
    // public void CreateBookTable(List<Book> bookList)
    // {
    //     BookTable = new ();
    //     BookTable.AddColumns("ID", "Title", "Author", "Genre", "Year", "Copies", "Score");
    //     foreach (Book book in bookList)
    //         {
    //             BookTable.AddRow(book.IdNumber.ToString(), book.Title, book.Author, book.Genre, book.Year.ToString(), book.Copies.ToString(), book.Score.ToString());
    //         }
    // }
    // public List<Book> GetBookList()
    // {
    //     bookData.GetRegisteredBooks();
    //     return bookData.BooksList;
    // }
}