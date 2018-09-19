using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatikaUsers.Model
{
    public class Software
    {
        public ulong Id { get; set; }
        public string Name { get; set; }

        public List<UserSoftware> Users { get; set; }
    }
}
