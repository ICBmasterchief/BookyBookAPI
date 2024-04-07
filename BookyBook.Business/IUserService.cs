using BookyBook.Data;
using BookyBook.Models;

namespace BookyBook.Business;
public interface IUserService
{
    public IEnumerable<User> GetAllUsers(UserQueryParameters? userQueryParameters = null);


    // public bool CheckExistingUserData(string? email, string? password, bool loggIn=false);
    // public bool LogIn();
    // public void SignUp();
    // public void CreateUser(string name, string email, string password, string checkPassword);
    // public bool CheckEmail(string email);
    // public void UpdateLoggedUserPenalty();
}