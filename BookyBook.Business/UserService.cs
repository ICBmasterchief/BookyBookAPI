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
    public IEnumerable<UserLogedDTO> GetAllUsers(UserQueryParameters? userQueryParameters, string? sortBy)
    {
        var users = _repository.GetAllUsers();

        var usersDTO = users.Select(u => new UserLogedDTO
        {
            UserId = u.IdNumber,
            UserName = u.Name.ToLower(),
            Email = u.Email.ToLower(),
            RegistrationDate = u.RegistrationDate,
            PenaltyFee = u.PenaltyFee,
            Role = u.Role,
        }).ToList();

        var query = usersDTO.AsQueryable();

        if (!string.IsNullOrWhiteSpace(userQueryParameters.Name))
        {
            query = query.Where(usr => usr.UserName.Contains(userQueryParameters.Name.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(userQueryParameters.Email))
        {
            query = query.Where(usr => usr.Email.Contains(userQueryParameters.Email.ToLower()));
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


        switch (sortBy.ToLower())
        {
        case "registrationdate":
            query = query.OrderBy(usr => usr.RegistrationDate);
            break;
        case "penaltyfee":
            query = query.OrderBy(usr => usr.PenaltyFee);
            break;
        default:
            break;
        }

        var result = query.ToList();

        return result;
    }
    public IEnumerable<Borrowing> GetBorrowingsByUserId(int userId, UserQueryParameters? userQueryParameters, string? sortBy)
    {
        var query = _repository.GetBorrowingsByUserId(userId).AsQueryable();

        if (userQueryParameters.fromDate.HasValue)
        {
            query = query.Where(t => t.BorrowingDate >= userQueryParameters.fromDate.Value);
        }

        if (userQueryParameters.toDate.HasValue)
        {
            query = query.Where(t => t.BorrowingDate <= userQueryParameters.toDate.Value);
        }


        switch (sortBy.ToLower())
        {
        case "borrowingdate":
            query = query.OrderBy(bw => bw.BorrowingDate);
            break;
        case "datetoreturn":
            query = query.OrderBy(bw => bw.DateToReturn);
            break;
        case "returneddate":
            query = query.OrderBy(bw => bw.ReturnedDate);
            break;
        case "penaltyfee":
            query = query.OrderBy(bw => bw.PenaltyFee);
            break;
        case "userid":
            query = query.OrderBy(bw => bw.UserId);
            break;
        case "bookid":
            query = query.OrderBy(bw => bw.BookId);
            break;
        default:
            break;
        }

        var result = query.ToList();

        return result;
    }

    public UserLogedDTO GetUser(int userId)
    {
        var user = _repository.GetUser(userId);
        var userDTO = new UserLogedDTO
        {
            UserId = user.IdNumber,
            UserName = user.Name,
            Email = user.Email,
            RegistrationDate = user.RegistrationDate,
            PenaltyFee = user.PenaltyFee,
            Role = user.Role,
        };
        return userDTO;
    }

    public void UpdateUser(int userId, UserUpdateDTO userUpdate)
    {
        var user = _repository.GetUser(userId);

        user.Name = userUpdate.Name;
        user.Password = userUpdate.Password;
        _repository.UpdateUser(user);
        _repository.SaveChanges();
    }

    public void PayPenaltyFee(int userId)
    {
        var user = _repository.GetUser(userId);

        user.PenaltyFee = 0;
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
