using BookyBook.Data;
using BookyBook.Models;

namespace BookyBook.Business;
public interface IBookService
{
    public Book AddBook(BookCreateDTO bookCreate);
    public IEnumerable<BookDTO> GetAllBooks(BookQueryParameters? bookQueryParameters, string? sortBy);
    public IEnumerable<Borrowing> GetBorrowingsByBookId(int bookId, BookQueryParameters? bookQueryParameters, string? sortBy);
    public BookDTO GetBook(int bookId);
    public void UpdateBook(int bookId, BookUpdateDTO bookUpdate);
    public void UpdateCopiesOfBook(int bookId, BookAddCopiesDTO bookAddCopies);
    public void DeleteBook(int bookId);

    // public void SearchForBooks();
    // public void ShowLibrary();
    // public void DonateBook();
    // public bool CheckExistingBookData(string? title, string? author);
    // public bool CheckExistingBookDataById(int bookId);
    // public void CreateBookTable(List<Book> bookList);
    // public List<Book> GetBookList();
}