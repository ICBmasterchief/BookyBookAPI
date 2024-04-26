using System.ComponentModel.DataAnnotations;

namespace BookyBook.Models;

public class BorrowingCreateDTO
{
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int BookId { get; set; }
    
}

