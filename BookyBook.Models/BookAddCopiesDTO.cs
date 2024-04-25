using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace BookyBook.Models;

public class BookAddCopiesDTO
{ 

    [Required]
    [Range(-1, 10, ErrorMessage = "No se permiten más de 10 libros a la vez")]
    public int Copies { get; set; }

}
