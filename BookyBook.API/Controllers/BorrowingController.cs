using Microsoft.AspNetCore.Mvc;
using BookyBook.Business;
using BookyBook.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class BorrowingController : ControllerBase
{

    private readonly ILogger<BorrowingController> _logger;
    private readonly IBorrowingService _borrowingService;
    private readonly IAuthService _authService;

    public BorrowingController(ILogger<BorrowingController> logger, IBorrowingService borrowingService, IAuthService authService)
    {
        _logger = logger;
        _borrowingService = borrowingService;
        _authService = authService;
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet(Name = "GetBorrowings")]
    public ActionResult<IEnumerable<Borrowing>> GetBorrowings([FromQuery] BorrowingQueryParameters borrowingQueryParameters, [FromQuery] string? sortBy)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try 
        {
            if (sortBy == null) {sortBy = "";}
            var borrowings = _borrowingService.GetAllBorrowings(borrowingQueryParameters, sortBy);
            return Ok(borrowings);
        }     
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
            return BadRequest("No se han encontrado libros");
        }
    }

    [Authorize(Roles = Roles.Admin)]
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

    //[Authorize(Roles = Roles.Admin)]
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
            _logger.LogInformation(ex.ToString());
            return BadRequest("No se ha podido crear el préstamo");
        }
        
    }
    
    //[Authorize(Roles = Roles.Admin)]
    [HttpPut("{borrowingId}")]
    public IActionResult UpdateBorrowing(int borrowingId, [FromBody] BorrowingUpdateDTO borrowingCreate)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); }
        try
        {
            var borrowing = _borrowingService.GetBorrowing(borrowingId);
            if (!_authService.HasAccessToResource(borrowing.UserId, HttpContext.User)) 
            {return Forbid(); }
            _borrowingService.UpdateBorrowing(borrowingId, borrowingCreate);
            return Ok(_borrowingService.GetBorrowing(borrowingId));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
           return NotFound("No se ha podido actualizar el préstamo");
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{borrowingId}")]
    public IActionResult DeleteBorrowing(int borrowingId)
    {
        try
        {
            _borrowingService.DeleteBorrowing(borrowingId);
            return Ok(_borrowingService.GetBorrowing(borrowingId));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
            return NotFound("No se ha podido eliminar el préstamo");
        }
    }

}