using System.ComponentModel.DataAnnotations;

namespace BookyBook.Models;

public class BorrowingQueryParameters
{

    [DataType(DataType.DateTime, ErrorMessage = "Las fechas de búsqueda tienen que ponerse en este formato: año,mes,dia (2024,04,08)")]
    public DateTime? fromDate { get; set; }

    [DataType(DataType.DateTime, ErrorMessage = "Las fechas de búsqueda tienen que ponerse en este formato: año,mes,dia (2024,04,08)")]
    public DateTime? toDate { get; set; }
    
    public bool Returned { get; set; }

    [Range(0.0, 1000.0, ErrorMessage = "La multa debe estar entre 0.00 y 1000.00")]
    public decimal fromPenaltyFee { get; set; }

    [Range(0.0, 1000.0, ErrorMessage = "La multa debe estar entre 0.00 y 1000.00")]
    public decimal toPenaltyFee { get; set; }

    [Range(1, 100000, ErrorMessage = "Los IDs deben ser mayor que 0 y menor que 100000")]
    public int UserId { get; set; }
    [Range(1, 100000, ErrorMessage = "Los IDs deben ser mayor que 0 y menor que 100000")]
    public int BookId { get; set; }

}
