using System.ComponentModel.DataAnnotations;

namespace BookyBook.Models;

public class UserCreateDTO
{
    [Required]
    [StringLength(100, ErrorMessage = "El nombre de Usuario debe tener menos de 100 caracteres")]
    public string Owner { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "El saldo inicial debe ser mayor que 0")]
    public decimal InitialBalance { get; set; }
}

