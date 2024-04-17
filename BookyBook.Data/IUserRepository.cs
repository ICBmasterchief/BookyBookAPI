using BookyBook.Models;

namespace BookyBook.Data;
public interface IUserRepository
{
    public void AddUser(User user);
    public IEnumerable<User> GetAllUsers(UserQueryParameters? userQueryParameters = null);
    public IEnumerable<Borrowing> GetBorrowingsByUserId(int userId, UserQueryParameters? userQueryParameters = null);
    public User GetUser(int userId);
    public void UpdateUser(User user);
    public void DeleteUser(int userId);
    public void SaveChanges();
    public UserDTOOut AddUserFromCredentials(UserDtoIn userDtoIn);
    public User GetUserFromCredentials(LoginDtoIn loginDtoIn);
}