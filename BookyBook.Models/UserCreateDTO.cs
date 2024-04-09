using System.ComponentModel.DataAnnotations;

namespace BookyBook.Models;

public class UserCreateDTO
{
    
    [Required]
    [StringLength(100, ErrorMessage = "El nombre de Usuario debe tener menos de 100 caracteres")]
    public string? Name { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Tienes que introducir un email con formato válido.")]
    [StringLength(100, ErrorMessage = "El email del usuario debe tener menos de 100 caracteres")]
    public string? Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "La contraseña del usuario debe tener menos de 100 caracteres")]
    public string? Password { get; set; }

}

