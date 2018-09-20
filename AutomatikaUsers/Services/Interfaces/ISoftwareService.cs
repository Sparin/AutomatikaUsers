using AutomatikaUsers.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatikaUsers.Services.Interfaces
{
    public interface ISoftwareService
    {
        SoftwareDTO AddSoftware(SoftwareDTO software);
        SoftwareDTO GetSoftware(int softwareId);
        IEnumerable<SoftwareDTO> GetSoftwares(int page);
        SoftwareDTO UpdateSoftware(SoftwareDTO software);
        void RemoveSoftware(int softwareId);
    }
}
