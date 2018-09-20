using AutomatikaUsers.Contexts;
using AutomatikaUsers.Model;
using AutomatikaUsers.Model.DTO;
using AutomatikaUsers.Services.Interfaces;
using AutomatikaUsers.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatikaUsers.Services
{
    public class UserService : IUserService
    {
        private const int ENTITIES_ON_PAGE = 50;

        private readonly ILogger<UserService> _logger;
        private readonly UserContext _userContext;

        public UserService(UserContext userContext, ILoggerFactory loggerFactory)
        {
            _userContext = userContext;
            _logger = loggerFactory.CreateLogger<UserService>();
        }

        public UserDTO AddUser(UserDTO user)
        {
            user.Id = 0;
            user.InstalledSoftware = null;
            User result = user;
            try
            {
                _userContext.Users.AddIfNotExists(result, x => x.IdentityName == user.IdentityName);
                _userContext.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError($"Error occured while adding new user to database\r\n{dbEx.Message}");
                throw;
            }
            return result;
        }

        public UserDTO GetUser(ulong userId)
        {
            var user = _userContext.Users
                .AsNoTracking()
                .Include(x => x.InstalledSoftware)
                .ThenInclude(x => x.Software)
                .SingleOrDefault(x => x.Id == userId);
            return user;
        }

        public IEnumerable<UserDTO> GetUsers(int page)
        {
            return _userContext.Users
                .AsNoTracking()
                .Include(x => x.InstalledSoftware)
                .ThenInclude(x => x.Software)
                .Skip(ENTITIES_ON_PAGE * page)
                .Take(ENTITIES_ON_PAGE)
                .Select(x => UserDTO.FromModel(x))
                .ToList();
        }

        public void RemoveUser(ulong userId)
        {
            try
            {
                var user = _userContext.Users.AsNoTracking().SingleOrDefault(x => x.Id == userId);
                if (user == null)
                    return;

            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError($"Error occurred while removing user from database\r\n{dbEx.Message}");
                throw;
            }
        }

        public UserDTO UpdateUser(UserDTO user)
        {
            var dbUser = _userContext.Users.SingleOrDefault(x => x.Id == user.Id);
            if (user == null)
                throw new ArgumentNullException("User doesn't exists in database");

            dbUser.Email = user.Email;
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;

            try
            {
                _userContext.Users.Update(user);
                _userContext.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError($"Error occurred while updating user in database\r\n{dbEx.Message}");
                throw;
            }

            return dbUser;
        }

        public void RemoveSoftwareLink(ulong userId, ulong softwareId)
        {
            try
            {
                var link = _userContext.UserSoftware
                    .AsNoTracking()
                    .SingleOrDefault(x => x.UserId == userId && x.SoftwareId == softwareId);
                if (link == null)
                    return;
                _userContext.Remove(link);
                _userContext.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError($"Cannot remove a link between user and software in database\r\n{dbEx.Message}");
                throw;
            }
        }

        public void AddSoftwareLink(ulong userId, ulong softwareId)
        {
            try
            {
                var user = _userContext.Users.AsNoTracking().SingleOrDefault(x => x.Id == userId);
                var software = _userContext.Software.AsNoTracking().SingleOrDefault(x => x.Id == softwareId);
                if (software == null)
                    throw new ArgumentNullException("Software doesn't exists in database");
                if (user == null)
                    throw new ArgumentNullException("User doesn't exists in database");

                var link = new UserSoftware() { UserId = userId, SoftwareId = softwareId };
                _userContext.UserSoftware.AddIfNotExists(link, x => x.UserId == userId && x.SoftwareId == softwareId);
                _userContext.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError($"Cannot add a link between user and software in database\r\n{dbEx.Message}");
                throw;
            }
        }
    }
}
