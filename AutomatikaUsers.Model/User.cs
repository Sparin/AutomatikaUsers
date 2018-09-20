using AutomatikaUsers.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatikaUsers.Model
{
    public class User
    {
        public int Id { get; set; }
        public string IdentityName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<UserSoftware> InstalledSoftware { get; set; }

        public static implicit operator User(UserDTO user)
        {
            return FromDTO(user);
        }

        public static User FromDTO(UserDTO user)
        {
            var result = new User()
            {
                Id = user.Id,
                IdentityName = user.IdentityName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return result;
        }
    }
}
