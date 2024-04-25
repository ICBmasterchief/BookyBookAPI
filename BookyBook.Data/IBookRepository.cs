using BookyBook.Models;

namespace BookyBook.Data;
public interface IBookRepository
{
    public void AddBook(Book book);
    public IEnumerable<Book> GetAllBooks(BookQueryParameters? bookQueryParameters = null);
    public IEnumerable<Borrowing> GetBorrowingsByBookId(int bookId);
    public Book GetBook(int bookId);
    public void UpdateBook(Book book);
    public void DeleteBook(int bookId);
    public void SaveChanges();
}