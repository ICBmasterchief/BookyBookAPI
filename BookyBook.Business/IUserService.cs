using BookyBook.Data;
using BookyBook.Models;
using System.Security.Claims;

namespace BookyBook.Business;
public interface IUserService
{
    public string AddUser(UserDtoIn userDTOIn);
    public IEnumerable<User> GetAllUsers(UserQueryParameters? userQueryParameters = null);
    public IEnumerable<Borrowing> GetBorrowingsByUserId(int userId, UserQueryParameters? userQueryParameters = null);
    public User GetUser(int userId);
    public void UpdateUser(int userId, UserUpdateDTO userUpdate);
    public void DeleteUser(int userId);
    public string Login(LoginDtoIn userDtoIn);
    public string GenerateToken(UserDTOOut userDTOOut);
    public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user);

    // public bool CheckExistingUserData(string? email, string? password, bool loggIn=false);
    // public bool LogIn();
    // public void SignUp();
    // public void CreateUser(string name, string email, string password, string checkPassword);
    // public bool CheckEmail(string email);
    // public void UpdateLoggedUserPenalty();
}