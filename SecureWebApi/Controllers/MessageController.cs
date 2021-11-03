using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureWebApi.Models;
using SecureWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult PublicMessage()
        {
            return Ok(new MessageResponse { Message = "This is not a secret..." });
        }

        [HttpGet("secret")]
        [Authorize(Roles = "Admin")]
        public IActionResult SecretMessage()
        {
            return Ok(new MessageResponse { Message = "This should be a secret..." });
        }
    }
}
