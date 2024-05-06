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

    public IEnumerable<User> GetAllUsers()
    {
        var users = _context.Users;
        if (users is null) {
            throw new InvalidOperationException("Error al intentar obtener los usuarios.");
        }

        return users;
    }

    public IEnumerable<Borrowing> GetBorrowingsByUserId(int userId)
    {
        var user = _context.Users
            .Include(usr => usr.Borrowings) // Incluir prestamos relacionados, pero ojo con referencia circular ;-)
            .FirstOrDefault(usr => usr.IdNumber == userId);

        var user1 = GetUser(userId);

        if (user1 is null) {
            throw new KeyNotFoundException("Usuario no encontrado.");
        } else if (user1.Borrowings is null || user1.Borrowings.Count == 0) {
            throw new KeyNotFoundException("No se encontraron préstamos.");
        }

        return user.Borrowings;
    }

    public User GetUser(int userId)
    {
        var user = _context.Users.FirstOrDefault(usr => usr.IdNumber == userId);
        if (user is null) {
            throw new InvalidOperationException("No se ha encontrado el usuario " + userId);
        }

        return user;
    }

    // public User GetUserByEmail(string email)
    // {
    //     var user = _context.Users.FirstOrDefault(usr => usr.Email == email);
    //     return user;
    // }

    public void UpdateUser(User user)
    {
        // En EF Core, si el objeto ya está siendo rastreado, actualizar sus propiedades
        // y llamar a SaveChanges() es suficiente para actualizarlo en la base de datos.
        // Asegúrate de que el estado del objeto sea 'Modified' si es necesario.
        _context.Entry(user).State = EntityState.Modified;
    }

    public void DeleteUser(int userId) 
    {
        var user = _context.Users.FirstOrDefault(usr => usr.IdNumber == userId);
        if (user is null) {
            throw new KeyNotFoundException("Usuario no encontrado.");
        }
        _context.Users.Remove(user);
        SaveChanges();
    }
    
    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public UserLogedDTO AddUserFromCredentials(UserCreateDTO userCreateDTO)
    {
        var user = new UserLogedDTO { UserName = userCreateDTO.UserName, Email = userCreateDTO.Email.ToLower(), Role = Roles.Guest};
        if (user == null)
        {
            throw new KeyNotFoundException("User not created.");
        }
        return user;
    }
    
    public User GetUserFromCredentials(LoginDTO loginDTO)
    {
        var user = _context.Users.FirstOrDefault(usr => usr.Email.ToLower() == loginDTO.Email.ToLower() && usr.Password == loginDTO.Password);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        return user;

    }

    public bool CheckExistingEmail(string email)
    {
        var existingUser = _context.Users.FirstOrDefault(usr => usr.Email.ToLower() == email.ToLower());
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