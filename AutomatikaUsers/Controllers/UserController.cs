using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomatikaUsers.Contexts;
using AutomatikaUsers.Model;
using AutomatikaUsers.Model.DTO;
using AutomatikaUsers.Services.Interfaces;
using AutomatikaUsers.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AutomatikaUsers.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILoggerFactory loggerFactory)
        {
            _userService = userService;
            _logger = loggerFactory.CreateLogger<UserController>();
        }

        // GET: api/<controller>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetUsers(int page = 0)
        {
            return Json(_userService.GetUsers(page));
        }

        // GET api/<controller>/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult GetUser(ulong id)
        {
            var result = _userService.GetUser(id);
            return Json(result);
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]UserDTO user)
        {
            try
            {
                return Json(_userService.AddUser(user));
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"Request ended with wrong parameters\r\n{ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult Put([FromBody]UserDTO user)
        {
            try
            {
                return Json(_userService.UpdateUser(user));
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"Request ended with wrong parameters\r\n{ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(ulong id)
        {
            _userService.RemoveUser(id);
        }
    }
}
