using AutomatikaUsers.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatikaUsers.Services.Interfaces
{
    public interface IUserService
    {
        UserDTO AddUser(UserDTO user);
        UserDTO GetUser(int userId);
        IEnumerable<UserDTO> GetUsers(int page);
        UserDTO UpdateUser(UserDTO user);
        void RemoveUser(int user);

        void AddSoftwareLink(int userId, int softwareId);
        void RemoveSoftwareLink(int userId, int softwareId);
    }
}
