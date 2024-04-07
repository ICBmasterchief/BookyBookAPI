using System.ComponentModel.DataAnnotations;

namespace BookyBook.Models;

public class UserUpdateDTO
{
    [Required]
    [StringLength(100, ErrorMessage = "El nombre del propietario debe tener menos de 100 caracteres")]
    public string Owner { get; set; }

}
