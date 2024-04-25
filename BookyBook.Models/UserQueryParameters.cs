using System.ComponentModel.DataAnnotations;

namespace BookyBook.Models;

public class UserQueryParameters
{

    [StringLength(100, ErrorMessage = "El nombre de Usuario debe tener menos de 100 caracteres")]
    public string? Name { get; set; }

    [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Tienes que introducir un email con formato válido.")]
    [StringLength(100, ErrorMessage = "El email del usuario debe tener menos de 100 caracteres")]
    public string? Email { get; set; }

    [DataType(DataType.DateTime, ErrorMessage = "Las fechas de búsqueda tienen que ponerse en este formato: año,mes,dia (2024,04,08)")]
    public DateTime? fromDate { get; set; }

    [DataType(DataType.DateTime, ErrorMessage = "Las fechas de búsqueda tienen que ponerse en este formato: año,mes,dia (2024,04,08)")]
    public DateTime? toDate { get; set; }

    // public UserQueryParameters (string? name, string? email)
    // {
    //     this.Name = name;
    //     this.Email = email;
    // }
    
}
