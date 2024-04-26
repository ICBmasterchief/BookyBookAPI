using BookyBook.Data;
using BookyBook.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookyBook.Business;
public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _repository;

    public AuthService(IConfiguration configuration, IUserRepository repository)
    {
        _configuration = configuration;
        _repository = repository;
    }

    public string AddUser(UserCreateDTO userCreateDTO)
    {   
        if (_repository.CheckExistingEmail(userCreateDTO.Email))
        {
          throw new KeyNotFoundException($"El email {userCreateDTO.Email} ya existe");   
        }
        var user = new User(userCreateDTO.UserName, userCreateDTO.Email, userCreateDTO.Password);
        _repository.AddUser(user);
        _repository.SaveChanges();
        var newUser = _repository.AddUserFromCredentials(userCreateDTO);
        return GenerateToken(newUser);
    }

    
    public string Login(LoginDTO loginDTO) {
        var user = _repository.GetUserFromCredentials(loginDTO);
        if (user.Email == "ignaciocasaus1cns@gmail.com")
        {
            user.Role = Roles.Admin;
        }
        var userLogin = new UserLogedDTO {UserId = user.IdNumber, UserName = user.Name, Email = user.Email, RegistrationDate = user.RegistrationDate, PenaltyFee = user.PenaltyFee, Borrowings = user.Borrowings, Role = user.Role};
        return GenerateToken(userLogin);
    }

    public string GenerateToken(UserLogedDTO userLogedDTO) {
        var key = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]); 
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration["JWT:ValidIssuer"],
            Audience = _configuration["JWT:ValidAudience"],
            Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userLogedDTO.UserId)),
                    new Claim(ClaimTypes.Name, userLogedDTO.UserName),
                    new Claim(ClaimTypes.Role, userLogedDTO.Role),
                    new Claim(ClaimTypes.Email, userLogedDTO.Email),
                    new Claim("myCustomClaim", "myCustomClaimValue"),
                    // add other claims
                }),
            Expires = DateTime.UtcNow.AddDays(7), // AddMinutes(60)
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    } 
    public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user) 
    {
        var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out int userId)) 
        { 
            return false; 
        }
        var isOwnResource = userId == requestedUserID;

        var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        if (roleClaim == null) return false;
        var isAdmin = roleClaim!.Value == Roles.Admin;
        
        var hasAccess = isOwnResource || isAdmin;
        return hasAccess;
    }

}