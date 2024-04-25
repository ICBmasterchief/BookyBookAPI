using Microsoft.AspNetCore.Mvc;
using BookyBook.Business;
using BookyBook.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Authorize]
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

    [Authorize(Roles = Roles.Admin)]
    [HttpGet(Name = "GetUsers")]
    public ActionResult<IEnumerable<UserLogedDTO>> GetUsers([FromQuery] UserQueryParameters userQueryParameters, [FromQuery] string? sortBy)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try 
        {
            if (sortBy == null) {sortBy = "";}
            var users = _userService.GetAllUsers(userQueryParameters, sortBy);
            return Ok(users);
        }     
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
            return BadRequest(ex.Message);
        }
    }

    
    [HttpGet("{userId}/borrowings", Name = "GetBorrowingsByUserId")]
    public IActionResult GetBorrowingsByUserId(int userId, [FromQuery] UserQueryParameters userQueryParameters, [FromQuery] string? sortBy)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        if (!_userService.HasAccessToResource(userId, HttpContext.User)) 
            {return Forbid(); }
        try
        {
            if (sortBy == null) {sortBy = "";}
            var borrowings = _userService.GetBorrowingsByUserId(userId, userQueryParameters, sortBy);
            return Ok(borrowings);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
            return BadRequest(ex.Message);
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
            _logger.LogInformation(ex.ToString());
           return NotFound(ex.Message);
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost()]
    public IActionResult CreateUser([FromBody] UserCreateDTO userCreateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); }
        try {
            _userService.AddUser(userCreateDTO);
            return Ok(userCreateDTO);
        }     
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
            return BadRequest(ex.Message);
        }
        
    }

    [HttpPut("{userId}")]
    public IActionResult UpdateUser(int userId, [FromBody] UserUpdateDTO userUpdate)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); }
        if (!_userService.HasAccessToResource(userId, HttpContext.User)) 
            {return Forbid(); }
        try
        {
            _userService.UpdateUser(userId, userUpdate);
            return Ok(_userService.GetUser(userId));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
            return NotFound("No encontrado el usuario " + userId);
        }
    }

    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        if (!_userService.HasAccessToResource(userId, HttpContext.User)) 
            {return Forbid(); }
        try
        {
            _userService.DeleteUser(userId);
            return Ok(_userService.GetUser(userId));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
            return NotFound("No encontrado el usuario " + userId);
        }
    }

}