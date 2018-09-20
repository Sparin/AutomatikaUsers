using AutomatikaUsers.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatikaUsers.Services.Interfaces
{
    public interface IUserService
    {
        //CRUD Methods
        UserDTO AddUser(UserDTO user);
        UserDTO GetUser(ulong userId);
        IEnumerable<UserDTO> GetUsers(int page);
        UserDTO UpdateUser(UserDTO user);
        void RemoveUser(ulong user);

        void AddSoftwareLink(ulong userId, ulong softwareId);
        void RemoveSoftwareLink(ulong userId, ulong softwareId);
    }
}
