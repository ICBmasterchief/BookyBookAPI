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
    private readonly IUserService _userService;

    public BookController(ILogger<BookController> logger, IBookService bookService)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetUsers")]
    public ActionResult<IEnumerable<User>> GetUsers([FromQuery] UserQueryParameters userQueryParameters)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try 
        {
            var users = _userService.GetAllUsers(userQueryParameters);
            return Ok(users);
        }     
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
            return BadRequest("No se han encontrado usuarios");
        }
    }
}