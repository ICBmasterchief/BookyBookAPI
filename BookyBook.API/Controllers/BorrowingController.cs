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
            _logger.LogInformation(ex.ToString());
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
            _logger.LogInformation(ex.ToString());
           return NotFound("No encontrado el libro " + borrowingId);
        }
    }
}