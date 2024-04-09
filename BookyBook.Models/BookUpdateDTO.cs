using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace BookyBook.Models;

public class BookUpdateDTO
{ 
    [Required]
    [StringLength(100, ErrorMessage = "El título del libro debe tener menos de 100 caracteres")]
    public string? Title { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "El autor del libro debe tener menos de 100 caracteres")]
    public string? Author { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "El género del libro debe tener menos de 100 caracteres")]
    public string? Genre { get; set; }
    [Required]
    [Range(-3000, 2024, ErrorMessage = "El año del libro debe estar entre -3000 y 2024")]
    public int? Year { get; set; }
    [Required]
    [Range(0.0, 10.0, ErrorMessage = "La puntuación del libro debe estar entre 0.0 y 10.0")]
    public decimal? Score { get; set; }
    
}
