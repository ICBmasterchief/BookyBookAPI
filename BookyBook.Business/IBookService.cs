using BookyBook.Data;
using BookyBook.Models;

namespace BookyBook.Business;
public interface IBookService
{
    public Book AddBook(BookCreateDTO bookCreate);
    public IEnumerable<Book> GetAllBooks(BookQueryParameters? bookQueryParameters = null);
    public IEnumerable<Borrowing> GetBorrowingsByBookId(int bookId, BookQueryParameters? bookQueryParameters = null);
    public Book GetBook(int bookId);
    public void UpdateBook(int bookId, BookUpdateDTO bookUpdate);
    public void DeleteBook(int bookId);

    // public void SearchForBooks();
    // public void ShowLibrary();
    // public void DonateBook();
    // public bool CheckExistingBookData(string? title, string? author);
    // public bool CheckExistingBookDataById(int bookId);
    // public void CreateBookTable(List<Book> bookList);
    // public List<Book> GetBookList();
}