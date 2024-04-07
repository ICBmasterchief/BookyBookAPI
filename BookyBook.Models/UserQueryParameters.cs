using System.ComponentModel.DataAnnotations;

namespace BookyBook.Models;

public class UserQueryParameters
{
    [StringLength(100, ErrorMessage = "El nombre de Usuario debe tener menos de 100 caracteres")]
    public string? Name { get; set; }

    [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Tienes que introducir un email con formato válido.")]
    [StringLength(100, ErrorMessage = "El email del usuario debe tener menos de 100 caracteres")]
    public string? Email { get; set; }

    // [Range(double.MinValue, double.MaxValue, ErrorMessage = "El balance debe estar en rango válido.")]
    // public decimal? Balance { get; set; }

    public DateTime? fromDate { get; set; }
    public DateTime? toDate { get; set; }
}
