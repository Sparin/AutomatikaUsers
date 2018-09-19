using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatikaUsers.Model
{
    public class User
    {
        public ulong Id { get; set; }
        public string Name { get; set; }

        public List<UserSoftware> InstalledSoftware { get; set; }
    }
}
