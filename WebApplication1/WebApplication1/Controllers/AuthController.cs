using System;

using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Interfaces;
using WebApplication1.Context;
using WebApplication1.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        AppDbContext _context;
        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpPost]
        public ActionResult<User> Post(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);

        }

        [HttpPost("login")]
        public ActionResult<string> login([FromBody]string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

            if (user == null)
                return NotFound("Usuário não encontrado");

            var token = GenerateToken(user);
            return Ok(token);


        }



        public static string GenerateToken(Models.User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("chave-muito-segura-confia");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)

                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
