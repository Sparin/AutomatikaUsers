using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AutomatikaUsers.Model.DTO
{
    public class SoftwareDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserDTO> Users { get; set; }

        public static implicit operator SoftwareDTO(Software software)
        {
            return FromModel(software);
        }

        public static SoftwareDTO FromModel(Software software)
        {
            var result = new SoftwareDTO()
            {
                Id = software.Id,
                Name = software.Name
            };

            if (software.Users != null && software.Users.Count > 0 && software.Users[0].User != null)
            {
                software.Users.ForEach(x => x.Software = null);
                result.Users = new List<UserDTO>();
                result.Users.AddRange(software.Users.Select(x => (UserDTO)x.User));
            }

            return result;
        }
    }
}
