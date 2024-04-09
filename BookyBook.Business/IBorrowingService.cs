using BookyBook.Data;
using BookyBook.Models;

namespace BookyBook.Business;
public interface IBorrowingService
{

    public void AddBorrowing(BorrowingCreateDTO borrowingCreate);
    public IEnumerable<Borrowing> GetAllBorrowings(BorrowingQueryParameters? borrowingQueryParameters = null);
    public Borrowing GetBorrowing(int borrowingId);
    public void UpdateBorrowing(int borrowingId, BorrowingUpdateDTO borrowingUpdate);
    public void DeleteBorrowing(int borrowingId);

    // public void BorrowBook();
    // public void DoBorrowing(int borrowIdBook);
    // public void ReturnBook();
    // public void CurrentBorrowedBooks();
    // public void BorrowedBooksHistory();
    // public void PayPenaltyFee();
    // public List<Borrowing> HasActiveBorrowings(int userId);
    // public void CreateBorrowingsTable(List<Borrowing> borrowingList, bool isCurrent);
    // public void UpdateBorrowingPenalty(int borrowingId, decimal penalty);
}