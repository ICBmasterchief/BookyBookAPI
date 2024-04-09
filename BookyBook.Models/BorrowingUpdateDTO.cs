using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace BookyBook.Models;

public class BorrowingUpdateDTO
{ 
   
   [DataType(DataType.DateTime, ErrorMessage = "La fecha de devolución tiene que ponerse en este formato: año,mes,dia (2024,04,08)")]
    public DateTime ReturnedDate { get; set; }

    public bool Returned { get; set; }
    
}
