using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServerLogicLibrary;
using TradingPolygon.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TradingPolygon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok("Kasim");
        }


        [HttpPost]
        public IActionResult UserLogin(IFormCollection collection)
        {
            try
            {
                LoginPersonModel person = new LoginPersonModel()
                {
                    Name = collection["Name"].ToString(),
                    Password = collection["Password"].ToString()
                };
                SqlCrud sqlCrud = new SqlCrud(GetConnectionString());

                bool isValid = sqlCrud.CheckIsUserValid(person.Name, person.Password);
                if (isValid)
                {
                    person.Id = Guid.NewGuid().ToString();
                    var token = GenerateJwtToken(person);
                    return RedirectToAction("Index", "Home", token);
                }
                return null;
            }
            catch (Exception e)
            {
                return BadRequest("Invalid username/password");
            }
        }

        private string GenerateJwtToken(LoginPersonModel person)
        {
            var securityKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,person.Id.ToString()),
                new Claim(ClaimTypes.Name,person.Name) 
            };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["jwt:Issuer"],
                claims,
                expires:DateTime.UtcNow.AddHours(5),
                signingCredentials:credentials       
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        private static string GetConnectionString(string connectionStringName = "Default")
        {
            string output = "";
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();
            output = config.GetConnectionString(connectionStringName);
            return output;
        }
    }
}
