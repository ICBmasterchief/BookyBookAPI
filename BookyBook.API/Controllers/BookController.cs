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
            _logger.LogInformation(ex.ToString());
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
            _logger.LogInformation(ex.ToString());
           return BadRequest("No se han encontrado préstamos");
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
            _logger.LogInformation(ex.ToString());
           return NotFound("No encontrado el libro " + bookId);
        }
    }
}