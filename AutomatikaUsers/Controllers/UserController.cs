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
        public ActionResult GetUser(int id)
        {
            try
            {
                var result = _userService.GetUser(id);
                return Json(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"Request ended with wrong parameters\r\n{ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult AddUser([FromBody]UserDTO user)
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

        // PUT api/<controller>
        [HttpPut]
        public ActionResult UpdateUser([FromBody]UserDTO user)
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
        public ActionResult RemoveUser(int id)
        {
            _userService.RemoveUser(id);
            return NoContent();
        }

        // POST api/<controller>/addSoftware
        [HttpPost("addSoftwareLink")]
        public ActionResult AddSoftwareLink([FromQuery]int userId, [FromQuery] int softwareId)
        {
            try
            {
                _userService.AddSoftwareLink(userId, softwareId);
                return StatusCode(201);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"Request ended with wrong parameters\r\n{ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("removeSoftwareLink")]
        public ActionResult RemoveSoftwareLink([FromQuery]int userId, [FromQuery] int softwareId)
        {
            try
            {
                _userService.RemoveSoftwareLink(userId, softwareId);
                return StatusCode(201);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"Request ended with wrong parameters\r\n{ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
