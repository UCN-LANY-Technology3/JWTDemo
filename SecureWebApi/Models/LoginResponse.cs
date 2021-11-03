﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebApi.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expires { get; internal set; }
    }
}
