using BookyBook.Models;

namespace BookyBook.Data;
public interface IBorrowingRepository
{
    public void AddBorrowing(Borrowing borrowing);
    public void GetRegisteredBorrowings();
    public void SaveBorrowingData();
}