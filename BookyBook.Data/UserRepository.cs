using BookyBook.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Runtime.InteropServices;

namespace BookyBook.Data;
public class UserRepository : IUserRepository
{

    private readonly BookyBookContext _context;

    public UserRepository(BookyBookContext context)
    {
        _context = context;
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
    }

    public IEnumerable<User> GetAllUsers(UserQueryParameters? userQueryParameters)
    {
        var users = _context.Users;
        if (users is null) {
            throw new InvalidOperationException("Error al intentar obtener los usuarios.");
        }
        return users;
    }

    public IEnumerable<Borrowing> GetBorrowingsByUserId(int userId, UserQueryParameters? userQueryParameters)
    {
        var user = _context.Users
            .Include(usr => usr.Borrowings) // Incluir prestamos relacionados, pero ojo con referencia circular ;-)
            .FirstOrDefault(usr => usr.IdNumber == userId);

        if (user is null) {
            throw new KeyNotFoundException("User not found.");
        }

        return user.Borrowings;
    }

    public User GetUser(int userId)
    {
        var user = _context.Users.FirstOrDefault(usr => usr.IdNumber == userId);
        return user;
    }

    public User GetUserByEmail(string email)
    {
        var user = _context.Users.FirstOrDefault(usr => usr.Email == email);
        return user;
    }

    public void UpdateUser(User user)
    {
        // En EF Core, si el objeto ya está siendo rastreado, actualizar sus propiedades
        // y llamar a SaveChanges() es suficiente para actualizarlo en la base de datos.
        // Asegúrate de que el estado del objeto sea 'Modified' si es necesario.
        _context.Entry(user).State = EntityState.Modified;
    }

    public void DeleteUser(int userId) 
    {
        var user = GetUser(userId);
        if (user is null) {
            throw new KeyNotFoundException("User not found.");
        }
        _context.Users.Remove(user);
        SaveChanges();
    }
    
    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public UserDTOOut AddUserFromCredentials(UserDtoIn userDtoIn)
    {
        var user = new UserDTOOut { UserName = userDtoIn.UserName, Email = userDtoIn.Email, Role = Roles.Guest};
        if (user == null)
        {
            throw new KeyNotFoundException("User not created.");
        }
        return user;
    }
    
    public User GetUserFromCredentials(LoginDtoIn loginDtoIn)
    {
        var user = _context.Users.FirstOrDefault(usr => usr.Email == loginDtoIn.Email && usr.Password == loginDtoIn.Password);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        return user;

    }

    public bool CheckExistingEmail(User user)
    {
        var existingUser = _context.Users.FirstOrDefault(usr => usr.Email == user.Email);
        if (existingUser == null)
        {
            return false;
        }
        return true;
    }

    // public List<User>? UsersList = new();
    // private readonly string UserJsonPath;
    // public UserRepository()
    // {
    //     if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    //     {
    //         UserJsonPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "BookyBook.Data", "Data.Users.json");
    //     } else {
    //         UserJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "BookyBook.Data", "Data.Users.json");
    //     }
    //     GetRegisteredUsers();
    // }
    // public void AddUser(User user)
    // {
    //     UsersList.Add(user);
    //     SaveUserData();
    // }
    // public void GetRegisteredUsers()
    // {
    //     try
    //     {
    //     string JsonUsers = File.ReadAllText(UserJsonPath);
    //     UsersList =  JsonSerializer.Deserialize<List<User>>(JsonUsers);
    //     } catch (System.Exception)
    //     {
    //         Console.WriteLine("ERROR TRYING ACCESS DATA");
    //     }
    // }
    // public void SaveUserData()
    // {
    //     string JsonUsers = JsonSerializer.Serialize(UsersList, new JsonSerializerOptions {WriteIndented = true});
    //     File.WriteAllText(UserJsonPath, JsonUsers);
    // }
}