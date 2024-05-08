using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
  public class UserContext : IUserContext
  {
    public ClaimsPrincipal Principal { get; set; }
  }
}
