using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookyBook.Models;
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdNumber { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public decimal PenaltyFee { get; set; } = 0;
    [JsonIgnore]
    public List<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    // ELIMINAR? private static int IdNumberSeed = 1111;
    public string Role { get; set; } = null;

    public User(){} 
    public User(string name, string email, string password){
        this.Name = name;
        this.Email = email;
        this.Password = password;
        this.RegistrationDate = DateTime.Today;
        //this.IdNumber = IdNumberSeed;
    }
    // public User(string name, string email, string password, int idNumber){
    //     this.Name = name;
    //     this.Email = email;
    //     this.Password = password;
    //     this.RegistrationDate = DateTime.Today;
    //     this.IdNumber = idNumber;
    // }
}
