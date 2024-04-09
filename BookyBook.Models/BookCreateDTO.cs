using System.ComponentModel.DataAnnotations;

namespace BookyBook.Models;

public class BookCreateDTO
{
    
    [Required]
    [StringLength(100, ErrorMessage = "El título del libro debe tener menos de 100 caracteres")]
    public string Title { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "El autor del libro debe tener menos de 100 caracteres")]
    public string Author { get; set; }

    [Required]
    [Range(1, 100, ErrorMessage = "La cantidad de copias del libro debe ser mínimo de 1 y máximo de 100")]
    public int Copies { get; set; }
    
}

