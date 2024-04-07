using System.ComponentModel.DataAnnotations;

namespace BookyBook.Models;
public class User
{
    [Key]
    public int IdNumber { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public decimal PenaltyFee { get; set; } = 0;
    public List<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    private static int IdNumberSeed = 1111;

    public User(){} 
    public User(string name, string email, string password){
        this.Name = name;
        this.Email = email;
        this.Password = password;
        this.RegistrationDate = DateTime.Today;
        this.IdNumber = IdNumberSeed;
    }
    public User(string name, string email, string password, int idNumber){
        this.Name = name;
        this.Email = email;
        this.Password = password;
        this.RegistrationDate = DateTime.Today;
        this.IdNumber = idNumber;
    }
}
