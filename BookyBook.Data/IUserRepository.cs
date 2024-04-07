using BookyBook.Models;

namespace BookyBook.Data;
public interface IUserRepository
{
    public void AddUser(User user);
    public IEnumerable<User> GetAllUsers(UserQueryParameters? userQueryParameters);
    public User GetUser(int userId);
    public void UpdateUser(User user);
    public void DeleteUser(int userId);
    public void SaveChanges();
}