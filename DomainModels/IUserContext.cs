using System.Security.Claims;

namespace DomainModels
{
  public interface IUserContext
  {
    ClaimsPrincipal Principal { get; set; }
  }
}