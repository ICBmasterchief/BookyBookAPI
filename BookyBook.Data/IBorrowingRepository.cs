using BookyBook.Models;

namespace BookyBook.Data;
public interface IBorrowingRepository
{
    public void AddBorrowing(Borrowing borrowing);
    public IEnumerable<Borrowing> GetAllBorrowings();
    public Borrowing GetBorrowing(int borrowingId);
    public void UpdateBorrowing(Borrowing borrowing);
    public void DeleteBorrowing(int borrowingId);
    public void SaveChanges();
}