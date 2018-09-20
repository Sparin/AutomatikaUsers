using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AutomatikaUsers.Model.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string IdentityName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<SoftwareDTO> InstalledSoftware { get; set; }

        public static implicit operator UserDTO(User user)
        {
            return FromModel(user);
        }

        public static UserDTO FromModel(User user)
        {
            var result = new UserDTO()
            {
                Id = user.Id,
                IdentityName = user.IdentityName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            if (user.InstalledSoftware != null && user.InstalledSoftware.Count > 0 && user.InstalledSoftware[0].Software != null)
            {
                user.InstalledSoftware.ForEach(x => x.User = null);
                result.InstalledSoftware = new List<SoftwareDTO>();
                result.InstalledSoftware.AddRange(user.InstalledSoftware.Select(x => (SoftwareDTO)x.Software));
            }

            return result;
        }
    }
}
