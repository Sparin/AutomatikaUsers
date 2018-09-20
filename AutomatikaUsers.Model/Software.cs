using AutomatikaUsers.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatikaUsers.Model
{
    public class Software
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserSoftware> Users { get; set; }

        public static implicit operator Software(SoftwareDTO software)
        {
            return FromDTO(software);
        }

        public static Software FromDTO(SoftwareDTO software)
        {
            var result = new Software()
            {
                Id = software.Id,
                Name = software.Name
            };

            return result;
        }
    }
}
