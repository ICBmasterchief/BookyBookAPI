using BookyBook.Data;
using BookyBook.Models;
using System.Security.Claims;

namespace BookyBook.Business;
public interface IUserService
{
    public IEnumerable<UserLogedDTO> GetAllUsers(UserQueryParameters? userQueryParameters, string? sortBy);
    public IEnumerable<Borrowing> GetBorrowingsByUserId(int userId, UserQueryParameters? userQueryParameters, string? sortBy);
    public UserLogedDTO GetUser(int userId);
    public void UpdateUser(int userId, UserUpdateDTO userUpdate);
    public void PayPenaltyFee(int userId);
    public void DeleteUser(int userId);
     

    // public bool CheckExistingUserData(string? email, string? password, bool loggIn=false);
    // public bool LogIn();
    // public void SignUp();
    // public void CreateUser(string name, string email, string password, string checkPassword);
    // public bool CheckEmail(string email);
    // public void UpdateLoggedUserPenalty();
}