using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatikaUsers.Model
{
    public class UserSoftware
    {
        public User User { get; set; }
        public int UserId { get; set; }

        public Software Software { get; set; }
        public int SoftwareId { get; set; }
    }
}
