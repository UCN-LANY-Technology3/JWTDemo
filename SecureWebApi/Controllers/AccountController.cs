﻿using Microsoft.AspNetCore.Http;
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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult Login(LoginRequest login)
        {
            var user = _accountService.Authenticate(login.Username, login.Password);
            if (user.IsAuthenticated)
            {
                var response = new LoginResponse
                {
                    Token = user.Token,
                    Expires = user.Expires,
                };
                return Ok(response);
            }
            return Unauthorized();
        }
    }
}
