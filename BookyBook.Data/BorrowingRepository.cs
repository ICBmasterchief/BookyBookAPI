using BookyBook.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Runtime.InteropServices;

namespace BookyBook.Data;
public class BorrowingRepository: IBorrowingRepository
{

    private readonly BookyBookContext _context;
    public BorrowingRepository(BookyBookContext context)
    {
        _context = context;
    }

    public void AddBorrowing(Borrowing borrowing)
    {
        _context.Borrowings.Add(borrowing);
    }

    public IEnumerable<Borrowing> GetAllBorrowings()
    {
        var borrowings = _context.Borrowings.ToList();
        if (borrowings is null) {
            throw new KeyNotFoundException("Error al intentar obtener los préstamos.");
        }
        return borrowings;
    }

    public Borrowing GetBorrowing(int borrowingId)
    {
        var borrowing = _context.Borrowings.FirstOrDefault(bk => bk.IdNumber == borrowingId);
        if (borrowing is null) {
            throw new KeyNotFoundException("No se ha encontrado el préstamo " + borrowingId);
        }
        return borrowing;
    }


    public void UpdateBorrowing(Borrowing borrowing)
    {
        // En EF Core, si el objeto ya está siendo rastreado, actualizar sus propiedades
        // y llamar a SaveChanges() es suficiente para actualizarlo en la base de datos.
        // Asegúrate de que el estado del objeto sea 'Modified' si es necesario.
        _context.Entry(borrowing).State = EntityState.Modified;
    }

    public void DeleteBorrowing(int borrowingId) 
    {
        var borrowing = GetBorrowing(borrowingId);
        if (borrowing is null) {
            throw new KeyNotFoundException("Préstamo no encontrado.");
        }
        _context.Borrowings.Remove(borrowing);
        SaveChanges();
    }
    
    public void SaveChanges()
    {
        _context.SaveChanges();
    }


    // public List<Borrowing>? BorrowingsList = new();
    // private readonly string BorrowingJsonPath;
    // public BorrowingRepository()
    // {
    //     if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    //     {
    //         BorrowingJsonPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "BookyBook.Data", "Data.Borrowings.json");
    //     } else {
    //         BorrowingJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "BookyBook.Data", "Data.Borrowings.json");
    //     }
    //     GetRegisteredBorrowings();
    // }
    // public void AddBorrowing(Borrowing borrowing)
    // {
    //     BorrowingsList.Add(borrowing);
    //     SaveBorrowingData();
    // }
    // public void GetRegisteredBorrowings()
    // {
    //     try
    //     {
    //     string JsonBorrowings = File.ReadAllText(BorrowingJsonPath);
    //     BorrowingsList =  JsonSerializer.Deserialize<List<Borrowing>>(JsonBorrowings);
    //     } catch (System.Exception)
    //     {
    //         Console.WriteLine("ERROR TRYING ACCESS DATA");
    //     }
    // }
    // public void SaveBorrowingData()
    // {
    //     string JsonBorrowings = JsonSerializer.Serialize(BorrowingsList, new JsonSerializerOptions {WriteIndented = true});
    //     File.WriteAllText(BorrowingJsonPath, JsonBorrowings);
    // }
}