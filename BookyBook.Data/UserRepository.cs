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

         public IEnumerable<User> GetAllUsers(UserQueryParameters? userQueryParameters) {

            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(userQueryParameters.Name))
            {
                query = query.Where(usr => usr.Name.Contains(userQueryParameters.Name));
            }

            if (!string.IsNullOrWhiteSpace(userQueryParameters.Email))
            {
                query = query.Where(usr => usr.Email.Contains(userQueryParameters.Email));
            }

            if (userQueryParameters.fromDate.HasValue && userQueryParameters.toDate.HasValue)
            {
                query = query.Where(usr => usr.RegistrationDate >= userQueryParameters.fromDate.Value 
                                        && usr.RegistrationDate <= userQueryParameters.toDate.Value);
            }
            else if (userQueryParameters.fromDate.HasValue)
            {
                query = query.Where(usr => usr.RegistrationDate >= userQueryParameters.fromDate.Value);
            }
            else if (userQueryParameters.toDate.HasValue)
            {
                query = query.Where(usr => usr.RegistrationDate <= userQueryParameters.toDate.Value);
            }

            var result = query.ToList();

            return result;
        }

        public User GetUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(usr => usr.IdNumber == userId);
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