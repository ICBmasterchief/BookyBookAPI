using Microsoft.AspNetCore.Mvc;
using BookyBook.Business;
using BookyBook.Models;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;

    public AuthController(ILogger<AuthController> logger, IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    [HttpPost("Login")]
    public ActionResult<string> Login([FromBody] LoginDTO loginDTO)
    {
        try
        {
            if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

            var token = _authService.Login(loginDTO);
            return Ok(token);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
            return Unauthorized(ex.Message);
            
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
            return BadRequest("Error generating the token");
        }
    }

    [HttpPost("Register")]
    public IActionResult Register([FromBody] UserCreateDTO userCreateDTO)
    {
        try
        {
            if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

            var token = _authService.AddUser(userCreateDTO);
            return Ok(token);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
            return BadRequest("Error generating the token");
        }
    }

}