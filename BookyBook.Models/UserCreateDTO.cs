using System.ComponentModel.DataAnnotations;

namespace BookyBook.Models;

public class UserCreateDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "El nombre de Usuario debe tener menos de 100 caracteres")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email is required")]
        [StringLength(100, ErrorMessage = "El email del usuario debe tener menos de 100 caracteres")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "La contraseña del usuario debe tener menos de 100 caracteres")]
        public string Password { get; set; }
}

