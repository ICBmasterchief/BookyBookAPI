using Microsoft.AspNetCore.Mvc;
using BookyBook.Business;
using BookyBook.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class BookController : ControllerBase
{

    private readonly ILogger<BookController> _logger;
    private readonly IBookService _bookService;

    public BookController(ILogger<BookController> logger, IBookService bookService)
    {
        _logger = logger;
        _bookService = bookService;
    }

    [AllowAnonymous]
    [HttpGet(Name = "GetBooks")]
    public ActionResult<IEnumerable<Book>> GetBooks([FromQuery] BookQueryParameters bookQueryParameters, [FromQuery] string? sortBy)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try 
        {
            if (sortBy == null) {sortBy = "";}
            var books = _bookService.GetAllBooks(bookQueryParameters, sortBy);
            return Ok(books);
        }     
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("{bookId}/borrowings", Name = "GetBorrowingsByBookId")]
    public IActionResult AdminGetBorrowingsByBookId(int bookId, [FromQuery] BookQueryParameters bookQueryParameters, [FromQuery] string? sortBy)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try
        {
            if (sortBy == null) {sortBy = "";}
            var borrowings = _bookService.GetBorrowingsByBookId(bookId, bookQueryParameters, sortBy);
            return Ok(borrowings);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
           return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet("{bookId}", Name = "GetBook")]
    public IActionResult GetBook(int bookId)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); }
        try
        {  
            var book = _bookService.GetBook(bookId);
            return Ok(book);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
           return NotFound("No se ha encontrado el libro " + bookId);
        }
    }

    [HttpPost()]
    public IActionResult CreateBook([FromBody] BookCreateDTO bookCreate)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try {
            var book = _bookService.AddBook(bookCreate);
            return Ok(book);
        }     
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
            return BadRequest(ex.Message);
        }
        
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{bookId}", Name = "UpdateBook")]
    public IActionResult AdminUpdateBook(int bookId, [FromBody] BookUpdateDTO bookUpdate)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try
        {
            _bookService.UpdateBook(bookId, bookUpdate);
            return Ok(_bookService.GetBook(bookId));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
           return NotFound(ex.Message);
        }
    }

    [HttpPut("{bookId}/copies")]
    public IActionResult UpdateCopiesOfBook(int bookId, [FromBody] BookAddCopiesDTO bookAddCopies)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try
        {
            _bookService.UpdateCopiesOfBook(bookId, bookAddCopies);
            return Ok(_bookService.GetBook(bookId));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
           return NotFound("No se ha podudo actualizar el libro.");
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{bookId}")]
    public IActionResult AdminDeleteBook(int bookId)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); }
        try
        {
            _bookService.DeleteBook(bookId);
            return Ok($"Libro {bookId} eliminado correctamente.");
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
            return NotFound("No se ha podido eliminar el libro.");
        }
    }
}