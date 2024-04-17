using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BookyBook.Data;
using BookyBook.Business;
using BookyBook.Models;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class BorrowingController : ControllerBase
{

    private readonly ILogger<BorrowingController> _logger;
    private readonly IBorrowingService _borrowingService;

    public BorrowingController(ILogger<BorrowingController> logger, IBorrowingService borrowingService)
    {
        _logger = logger;
        _borrowingService = borrowingService;
    }

    [HttpGet(Name = "GetBorrowings")]
    public ActionResult<IEnumerable<Borrowing>> GetBorrowings([FromQuery] BorrowingQueryParameters borrowingQueryParameters)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try 
        {
            var borrowings = _borrowingService.GetAllBorrowings(borrowingQueryParameters);
            return Ok(borrowings);
        }     
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest("No se han encontrado libros");
        }
    }


    [HttpGet("{borrowingId}", Name = "GetBorrowing")]
    public IActionResult GetBorrowing(int borrowingId)
    {
        try
        {
            if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

            var borrowing = _borrowingService.GetBorrowing(borrowingId);
            return Ok(borrowing);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
           return NotFound("No encontrado el libro " + borrowingId);
        }
    }

    [HttpPost()]
    public IActionResult CreateBorrowing([FromBody] BorrowingCreateDTO borrowingCreate)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try {
            var borrowing = _borrowingService.AddBorrowing(borrowingCreate);
            return Ok(borrowing);
        }     
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest("No se ha podido crear el préstamo");
        }
        
    }

    [HttpPut("{borrowingId}")]
    public IActionResult UpdateBorrowing(int borrowingId, [FromBody] BorrowingUpdateDTO borrowingCreate)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try
        {
            _borrowingService.UpdateBorrowing(borrowingId, borrowingCreate);
            //return NoContent();
            return Ok(_borrowingService.GetBorrowing(borrowingId));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
           return NotFound("No se ha podido actualizar el préstamo");
        }
    }

    [HttpDelete("{borrowingId}")]
    public IActionResult DeleteBorrowing(int borrowingId)
    {
        try
        {
            _borrowingService.DeleteBorrowing(borrowingId);
            //return NoContent();
            return Ok(_borrowingService.GetBorrowing(borrowingId));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
            return NotFound("No se ha podido eliminar el préstamo");
        }
    }

}