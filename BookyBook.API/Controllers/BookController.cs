using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BookyBook.Data;
using BookyBook.Business;
using BookyBook.Models;

namespace API.Controllers;

[ApiController]
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

    [HttpGet(Name = "GetBooks")]
    public ActionResult<IEnumerable<Book>> GetBooks([FromQuery] BookQueryParameters bookQueryParameters)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try 
        {
            var books = _bookService.GetAllBooks(bookQueryParameters);
            return Ok(books);
        }     
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest("No se han encontrado libros");
        }
    }

    [HttpGet("{bookId}/borrowings", Name = "GetBorrowingsByBookId")]
    public IActionResult GetBorrowingsByBookId(int bookId, [FromQuery] BookQueryParameters bookQueryParameters)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try
        {
            var borrowings = _bookService.GetBorrowingsByBookId(bookId, bookQueryParameters);
            return Ok(borrowings);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
           return BadRequest("No se han encontrado préstamos de ese libro");
        }
    }

    [HttpGet("{bookId}", Name = "GetBook")]
    public IActionResult GetBook(int bookId)
    {
        try
        {
            if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

            var book = _bookService.GetBook(bookId);

            return Ok(book);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
           return NotFound("No se ha encontrado el libro " + bookId);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogInformation(ex.Message);
            return NotFound("No se ha encontrado el libro " + bookId);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest("Error obteniendo el libro " + bookId);
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
            _logger.LogInformation(ex.Message);
            return BadRequest("No se ha podido crear el libro");
        }
        
    }

    [HttpPut("{bookId}")]
    public IActionResult UpdateBook(int bookId, [FromBody] BookUpdateDTO bookUpdate)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try
        {
            _bookService.UpdateBook(bookId, bookUpdate);
            //return NoContent();
            return Ok(_bookService.GetBook(bookId));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
           return NotFound("No se ha podudo actualizar el libro");
        }
    }

    [HttpDelete("{bookId}")]
    public IActionResult DeleteBook(int bookId)
    {
        try
        {
            _bookService.DeleteBook(bookId);
            //return NoContent();
            return Ok(_bookService.GetBook(bookId));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
            return NotFound("No se ha podido eliminar el libro");
        }
    }
}