using System.ComponentModel.DataAnnotations;

namespace BookyBook.Models;

public class BookQueryParameters
{

    [StringLength(100, ErrorMessage = "El título del libro debe tener menos de 100 caracteres")]
    public string? Title { get; set; }

    [StringLength(100, ErrorMessage = "El autor del libro debe tener menos de 100 caracteres")]
    public string? Author { get; set; }

    [StringLength(100, ErrorMessage = "El género del libro debe tener menos de 100 caracteres")]
    public string? Genre { get; set; }

    [Range(-3000, 2024, ErrorMessage = "El año debe estar entre -3000 y 2024")]
    public int? fromYear { get; set; }

    [Range(-3000, 2024, ErrorMessage = "El año debe estar entre -3000 y 2024")]
    public int? toYear { get; set; }
    
    [Range(0.0, 10.0, ErrorMessage = "La puntuación del libro debe estar entre 0.0 y 10.0")]
    public decimal? Score { get; set; }

}
