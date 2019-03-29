using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace BJ.WEB.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public string UserId
        {
            get
            {
                return User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            }
        }
       
    }
}
