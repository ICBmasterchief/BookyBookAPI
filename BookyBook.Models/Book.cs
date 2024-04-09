namespace BookyBook.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdNumber { get; set; }
    [Required]
    public string? Title { get; set; }
    [Required]
    public string? Author { get; set; }
    public string? Genre { get; set; }
    public int? Year { get; set; }
    [Required]
    public int? Copies { get; set; }
    public decimal? Score { get; set; }
    public List<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    // ELIMINAR? private static int IdNumberSeed = 10001;

    public Book(){}
    public Book(string title, string author, string genre, int year, int copies, decimal score){
        this.Title = title;
        this.Author = author;
        this.Genre = genre;
        this.Year = year;
        this.Copies = copies;
        this.Score = score;
        // this.IdNumber = IdNumberSeed;
    }
    // public Book(string title, string author, string genre, int year, int copies, decimal score, int idNumber){
    //     this.Title = title;
    //     this.Author = author;
    //     this.Genre = genre;
    //     this.Year = year;
    //     this.Copies = copies;
    //     this.Score = score;
    //     this.IdNumber = idNumber;
    // }
}
