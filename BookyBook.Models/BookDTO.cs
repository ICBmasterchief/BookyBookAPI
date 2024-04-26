using System.ComponentModel.DataAnnotations;

namespace BookyBook.Models;

public class BookDTO
{
    [Required]
    public int BookId { get; set; }
    [Required]
    public string Title { get; set; }

    [Required]
    public string Author { get; set; }

    [Required]
    public string? Genre { get; set; } = null;

    [Required]
    public int? Year { get; set; } = null;

    [Required]
    public int Copies { get; set; }

    [Required]
    public decimal? Score { get; set; } = null;
    
}

