using BookyBook.Models;

namespace BookyBook.Data;
public interface IUserRepository
{
    public void AddUser(User user);
    public IEnumerable<User> GetAllUsers();
    public IEnumerable<Borrowing> GetBorrowingsByUserId(int userId);
    public User GetUser(int userId);
    public void UpdateUser(User user);
    public void DeleteUser(int userId);
    public void SaveChanges();
    public UserLogedDTO AddUserFromCredentials(UserCreateDTO userCreateDTO);
    public User GetUserFromCredentials(LoginDTO loginDTO);
    public bool CheckExistingEmail(string email);
}