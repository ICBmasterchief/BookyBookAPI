using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using BookyBook.Data;
using BookyBook.Models;
//using Spectre.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookyBook.Business;
public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _repository;

    public UserService(IConfiguration configuration, IUserRepository repository)
    {
        _configuration = configuration;
        _repository = repository;
    }
    public IEnumerable<User> GetAllUsers(UserQueryParameters? userQueryParameters)
    {
        var query = _repository.GetAllUsers(userQueryParameters).AsQueryable();

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
    public IEnumerable<Borrowing> GetBorrowingsByUserId(int userId, UserQueryParameters? userQueryParameters)
    {
        var query = _repository.GetBorrowingsByUserId(userId, userQueryParameters).AsQueryable();

        if (userQueryParameters.fromDate.HasValue)
        {
            query = query.Where(t => t.BorrowingDate >= userQueryParameters.fromDate.Value);
        }

        if (userQueryParameters.toDate.HasValue)
        {
            query = query.Where(t => t.BorrowingDate <= userQueryParameters.toDate.Value);
        }

        var result = query.ToList();

        return result;
    }

    public User GetUser(int userId)
    {
        return _repository.GetUser(userId);
    }

    public string AddUser(UserDtoIn userDTOIn)
    {   
        var parameter = new UserQueryParameters (userDTOIn.UserName, userDTOIn.Email);
        if (GetAllUsers(parameter) != null)
        {
          throw new KeyNotFoundException($"El email {userDTOIn.Email} ya existe");   
        }
        var user = new User(userDTOIn.UserName, userDTOIn.Email, userDTOIn.Password);
        _repository.AddUser(user);
        _repository.SaveChanges();
        var newUser = _repository.AddUserFromCredentials(userDTOIn);
        return GenerateToken(newUser);
    }

    public void UpdateUser(int userId, UserUpdateDTO userUpdate)
    {
        var user = _repository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario {userId} no encontrado.");
        }

        user.Name = userUpdate.Name;
        user.Password = userUpdate.Password;
        _repository.UpdateUser(user);
        _repository.SaveChanges();
    }

    public void DeleteUser(int userId)
    {
        var user = _repository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario {userId} no encontrado.");
        }

        _repository.DeleteUser(userId);

    }

    
    public string Login(LoginDtoIn loginDtoIn) {
        var user = _repository.GetUserFromCredentials(loginDtoIn);
        if (user.Email == "ignaciocasaus1cns@gmail.com")
        {
            user.Role = Roles.Admin;
        }
        var userLogin = new UserDTOOut {UserId = user.IdNumber, UserName = user.Name, Email = user.Email, RegistrationDate = user.RegistrationDate, PenaltyFee = user.PenaltyFee, Borrowings = user.Borrowings, Role = user.Role};
        return GenerateToken(userLogin);
    }

    public string GenerateToken(UserDTOOut userDTOOut) {
        var key = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]); 
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration["JWT:ValidIssuer"],
            Audience = _configuration["JWT:ValidAudience"],
            Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userDTOOut.UserId)),
                    new Claim(ClaimTypes.Name, userDTOOut.UserName),
                    new Claim(ClaimTypes.Role, userDTOOut.Role),
                    new Claim(ClaimTypes.Email, userDTOOut.Email),
                    new Claim("myCustomClaim", "myCustomClaimValue"),
                    // add other claims
                }),
            Expires = DateTime.UtcNow.AddDays(7), // AddMinutes(60)
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    } 
    public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user) 
    {
        var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out int userId)) 
        { 
            return false; 
        }
        var isOwnResource = userId == requestedUserID;

        var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        if (roleClaim != null) return false;
        var isAdmin = roleClaim!.Value == Roles.Admin;
        
        var hasAccess = isOwnResource || isAdmin;
        return hasAccess;
    }



    // public readonly UserData userData = new();
    // public static User LoggedUser;
    // public bool CheckExistingUserData(string? email, string? password, bool loggIn=false)
    // {
    //     foreach (var user in userData.UsersList)
    //     {
    //         if (loggIn)
    //         {
    //             if (user.Email == email && user.Password == password)
    //             {
    //                 LoggedUser = user;
    //                 return true;
    //             }
    //         } else if (user.Email == email)
    //         {
    //             AnsiConsole.MarkupLine("[yellow]Email already in use.[/]");
    //             return true;
    //         }
    //     }
    //     return false;
    // }
    // public bool LogIn()
    // {
    //     AnsiConsole.MarkupLine("[green]Log in[/]");
    //     string email = AnsiConsole.Ask<String>("Email:").ToLower();
    //     string password = AnsiConsole.Prompt(new TextPrompt<string>("Password:").Secret());
    //     if (CheckExistingUserData(email, password, true))
    //     {
    //         AnsiConsole.MarkupLine("[yellow]You are logging in.[/]");
    //         return true;
    //     }
    //     AnsiConsole.MarkupLine("[yellow]Invalid Email or Password.[/]");
    //     return false;
    // }
    //  public void SignUp()
    // {
    //     AnsiConsole.MarkupLine("[green]Creating new user[/]");
    //     AnsiConsole.MarkupLine("");
    //     string name = AnsiConsole.Ask<String>("User Name:").ToLower();
    //     string email = AnsiConsole.Ask<String>("Email:").ToLower();
    //     if (CheckEmail(email))
    //     {
    //         string password = AnsiConsole.Prompt(new TextPrompt<string>("Password:").Secret());
    //         string checkPassword = AnsiConsole.Prompt(new TextPrompt<string>("Repeat Password:").Secret());
    //         CreateUser(name, email, password, checkPassword);
    //     } else {
    //         AnsiConsole.MarkupLine("[yellow]Invalid email format.[/]");
    //     }
    //     Thread.Sleep(3000);
    // }
    // public void CreateUser(string name, string email, string password, string checkPassword)
    // {
    //     if(password == checkPassword)
    //     {
    //         if (userData.UsersList.Count == 0)
    //         {
    //             User user = new(name, email, password);
    //             userData.AddUser(user);
    //             AnsiConsole.MarkupLine("[yellow]User created succesfully![/]");
    //         } else if (CheckExistingUserData(email, null) == false)
    //         {
    //                 int num = userData.UsersList.Last().IdNumber;
    //                 num++;
    //                 User user = new(name, email, password, num);
    //                 userData.AddUser(user);
    //                 AnsiConsole.MarkupLine("[yellow]User created succesfully![/]");
    //         }

    //     } else {
    //         AnsiConsole.MarkupLine("[yellow]ERROR: Passwords do not match.[/]");
    //     }
    // }
    // public bool CheckEmail(string email)
    // {
    //     string emailPatron = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
    //     Regex regex = new Regex(emailPatron);
    //     return regex.IsMatch(email);
    // }
    // public void UpdateLoggedUserPenalty()
    // {
    //     if (userData.UsersList.Find(x => x.IdNumber == LoggedUser.IdNumber) != null)
    //     {
    //         userData.UsersList.Find(x => x.IdNumber == LoggedUser.IdNumber).PenaltyFee = LoggedUser.PenaltyFee;
    //     }
    // }
}
