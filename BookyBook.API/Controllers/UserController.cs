using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BookyBook.Data;
using BookyBook.Business;
using BookyBook.Models;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
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
            _logger.LogInformation(ex.Message);
            return BadRequest("No se han encontrado usuarios");
        }
    }

    [HttpGet("{userId}/borrowings", Name = "GetBorrowingsByUserId")]
    public IActionResult GetBorrowingsByUserId(int userId, [FromQuery] UserQueryParameters userQueryParameters)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try
        {
            var borrowings = _userService.GetBorrowingsByUserId(userId, userQueryParameters);
            return Ok(borrowings);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest("No se han encontrado préstamos");
        }
    }

    [HttpGet("{userId}", Name = "GetUser")]
    public IActionResult GetUser(int userId)
    {
        try
        {
            if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

            var user = _userService.GetUser(userId);
            return Ok(user);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
           return NotFound("No encontrado el usuario " + userId);
        }
    }

    [HttpPost()]
    public IActionResult CreateUser([FromBody] UserDtoIn userDtoIn)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try {
            _userService.AddUser(userDtoIn);
            return Ok(userDtoIn);
        }     
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest("No se ha podido crear el usuario");
        }
        
    }

    [HttpPut("{userId}")]
    public IActionResult UpdateUser(int userId, [FromBody] UserUpdateDTO userUpdate)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try
        {
            _userService.UpdateUser(userId, userUpdate);
            //return NoContent();
            return Ok(_userService.GetUser(userId));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
            return NotFound("No encontrado el usuario " + userId);
        }
    }

    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        try
        {
            _userService.DeleteUser(userId);
            //return NoContent();
            return Ok(_userService.GetUser(userId));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
            return NotFound("No encontrado el usuario " + userId);
        }
    }

}