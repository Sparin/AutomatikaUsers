using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomatikaUsers.Contexts;
using AutomatikaUsers.Model;
using AutomatikaUsers.Model.DTO;
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
        private readonly UserContext _userContext;
        private readonly ILogger<SoftwareController> _logger;

        public SoftwareController(UserContext userContext, ILoggerFactory loggerFactory)
        {
            _userContext = userContext;
            _logger = loggerFactory.CreateLogger<SoftwareController>();
        }

        // GET: api/<controller>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetSoftware(int page = 0)
        {
            throw new NotImplementedException();
        }

        // GET api/<controller>/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult GetSoftware(ulong id)
        {
            throw new NotImplementedException();
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]Software software)
        {
            throw new NotImplementedException();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult Put(ulong id, [FromBody]Software softwareUpdated)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(ulong id)
        {
            throw new NotImplementedException();
        }
    }
}
