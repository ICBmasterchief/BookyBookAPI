using System.ComponentModel.DataAnnotations;

namespace BookyBook.Models;

public class UserUpdateDTO
{
    
    [Required]
    [StringLength(100, ErrorMessage = "El nombre de Usuario debe tener menos de 100 caracteres")]
    public string Name { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "La contraseña del usuario debe tener menos de 100 caracteres")]
    public string Password { get; set; }

}
