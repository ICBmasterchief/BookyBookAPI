using BookyBook.Data;
using BookyBook.Models;
using System.Security.Claims;

namespace BookyBook.Business;
public interface IAuthService
{
    public string AddUser(UserCreateDTO userCreateDTO);
    public string Login(LoginDTO loginDTO);
    public string GenerateToken(UserLogedDTO userLogedDTO);
    public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user);
    public int GetUserClaimId(ClaimsPrincipal user);
}