﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SecureWebApi.Services.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecureWebApi.Services
{
    public interface IAccountService
    {
        Userinfo Authenticate(string username, string password);
    }

    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;

        public AccountService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Userinfo Authenticate(string username, string password)
        {
            var expires = DateTime.Now.AddDays(1);

            var user = new Userinfo
            {
                IsAuthenticated = true,
                Username = username,
                Expires = expires,
            };
            user.Token = GenerateJwt(user, expires);
            return user;
        }

        private string GenerateJwt(Userinfo userinfo, DateTime expires)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userinfo.Username),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["JwtSettings:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Exp, expires.ToString()),
            };

            var identity = new ClaimsIdentity(claims, "Token");
            identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var handler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            };
            return handler.CreateEncodedJwt(descriptor);
        }
    }
}
