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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AutomatikaUsers.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class SoftwareController : Controller
    {
        private readonly ISoftwareService _softwareService;
        private readonly ILogger<SoftwareController> _logger;

        public SoftwareController(ISoftwareService softwareService, ILoggerFactory loggerFactory)
        {
            _softwareService = softwareService;
            _logger = loggerFactory.CreateLogger<SoftwareController>();
        }

        // GET: api/<controller>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetSoftwares(int page = 0)
        {
            return Json(_softwareService.GetSoftwares(page));
        }

        // GET api/<controller>/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult GetSoftware(int id)
        {
            try
            {
                var result = _softwareService.GetSoftware(id);
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
        public ActionResult AddSoftware([FromBody]Software software)
        {
            try
            {
                return Json(_softwareService.AddSoftware(software));
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"Request ended with wrong parameters\r\n{ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public ActionResult Put([FromBody]Software software)
        {
            try
            {
                return Json(_softwareService.UpdateSoftware(software));
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"Request ended with wrong parameters\r\n{ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _softwareService.RemoveSoftware(id);
            return NoContent();
        }
    }
}
