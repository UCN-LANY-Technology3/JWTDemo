using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebApi.Services.Models
{
    public class Userinfo
    {
        public string Username { get; set; }
        public bool IsAuthenticated { get; set; }
        public DateTime Expires { get; set; }
        public string Token { get; set; }
    }
}
