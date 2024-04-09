using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using BookyBook.Data;
using BookyBook.Models;
//using Spectre.Console;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace BookyBook.Business;
public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }
    public IEnumerable<User> GetAllUsers(UserQueryParameters? userQueryParameters)
    {
        return _repository.GetAllUsers(userQueryParameters);
    }
    public IEnumerable<Borrowing> GetBorrowingsByUserId(int userId, UserQueryParameters? userQueryParameters)
    {
        return _repository.GetBorrowingsByUserId(userId, userQueryParameters);
    }

    public User GetUser(int userId)
    {
        return _repository.GetUser(userId);
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

    public void AddUser(UserCreateDTO userCreate)
    {

    }

    public void DeleteUser(int userId)
    {

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
