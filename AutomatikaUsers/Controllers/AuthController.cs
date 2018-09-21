using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomatikaUsers.Contexts;
using AutomatikaUsers.Model.DTO;
using AutomatikaUsers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AutomatikaUsers.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly UserContext _userContext;

        public AuthController(IUserService userService, UserContext userContext, ILoggerFactory loggerFactory)
        {
            _userService = userService;
            _userContext = userContext;
            _logger = loggerFactory.CreateLogger<UserController>();
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult Get()
        {
            if (!_userContext.Users.Any(x => x.IdentityName == this.User.Identity.Name) && this.User.Identity.IsAuthenticated)
                _userService.AddUser(new UserDTO() { IdentityName = this.User.Identity.Name });

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
