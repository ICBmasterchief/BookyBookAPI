using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookyBook.Models;

public class LoginDTO
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "La contraseña del usuario debe tener menos de 100 caracteres")]
        public string Password { get; set; }
}

