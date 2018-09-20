using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomatikaUsers.Contexts;
using AutomatikaUsers.Model;
using AutomatikaUsers.Model.DTO;
using AutomatikaUsers.Services.Interfaces;
using AutomatikaUsers.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AutomatikaUsers.Services
{
    public class SoftwareService : ISoftwareService
    {
        private const int ENTITIES_ON_PAGE = 50;

        private readonly ILogger<UserService> _logger;
        private readonly UserContext _userContext;

        public SoftwareService(UserContext userContext, ILoggerFactory loggerFactory)
        {
            _userContext = userContext;
            _logger = loggerFactory.CreateLogger<UserService>();
        }

        public SoftwareDTO AddSoftware(SoftwareDTO software)
        {
            software.Id = 0;
            Software result = software;
            try
            {
                _userContext.Software.AddIfNotExists(result, x => x.Name == software.Name);
                _userContext.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError($"Error occured while adding new user to database\r\n{dbEx.Message}");
                throw;
            }
            return result;
        }

        public SoftwareDTO GetSoftware(ulong softwareId)
        {
            return _userContext.Software
                .AsNoTracking()
                .Include(x => x.Users)
                .ThenInclude(x => x.User)
                .SingleOrDefault(x => x.Id == softwareId);
        }

        public IEnumerable<SoftwareDTO> GetSoftware(int page)
        {
            return _userContext.Software
                .AsNoTracking()
                .Include(x => x.Users)
                .ThenInclude(x => x.User)
                .Skip(ENTITIES_ON_PAGE * page)
                .Take(ENTITIES_ON_PAGE)
                .Select(x => SoftwareDTO.FromModel(x));
        }

        public void RemoveSoftware(ulong softwareId)
        {
            try
            {
                var software = _userContext.Software.AsNoTracking().SingleOrDefault(x => x.Id == softwareId);
                if (software == null)
                    return;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError($"Error occurred while removing software from database\r\n{dbEx.Message}");
                throw;
            }
        }

        public SoftwareDTO UpdateSoftware(SoftwareDTO software)
        {
            var dbSoftware = _userContext.Software.SingleOrDefault(x => x.Id == software.Id);
            if (software == null)
                throw new ArgumentNullException("User doesn't exists in database");

            dbSoftware.Name = software.Name;

            try
            {
                _userContext.Software.Update(dbSoftware);
                _userContext.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError($"Error occurred while updating software in database\r\n{dbEx.Message}");
                throw;
            }

            return dbSoftware;
        }
    }
}
