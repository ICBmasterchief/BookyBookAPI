using BookyBook.Models;

namespace BookyBook.Data;
public interface IBookRepository
{
    public void AddBook(Book book);
    public void GetRegisteredBooks();
    public void SaveBookData();
}