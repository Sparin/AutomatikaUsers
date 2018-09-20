using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AutomatikaUsers.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public ActionResult Get()
        {
            var claim = this.User.Claims.FirstOrDefault();
            return Json(new
            {
                IdentityName = this.User.Identity.Name,
                this.User.Identity.IsAuthenticated,
                claim?.Issuer,
                claim?.Value
            });
        }
    }
}
