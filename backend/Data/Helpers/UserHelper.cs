using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Data
{
    public class UserHelper
    {
        public int id { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string oldPassword { get; set; }
        public List<string> roles { get; set; }
    }
}
