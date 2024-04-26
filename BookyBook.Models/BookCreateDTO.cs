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

    [StringLength(100, ErrorMessage = "El género del libro debe tener menos de 100 caracteres")]
    public string? Genre { get; set; } = null;

    [Range(-3000, 2024, ErrorMessage = "El año debe estar entre -3000 y 2024")]
    public int? Year { get; set; } = null;

    [Required]
    [Range(1, 100, ErrorMessage = "La cantidad de copias del libro debe ser mínimo de 1 y máximo de 100")]
    public int Copies { get; set; }

    [Range(0.0, 10.0, ErrorMessage = "La puntuación del libro debe estar entre 0.0 y 10.0")]
    public decimal? Score { get; set; } = null;
    
}

