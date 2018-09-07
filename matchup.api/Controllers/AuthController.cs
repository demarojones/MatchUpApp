using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using matchup.api.Data;
using matchup.api.Entities;
using matchup.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace matchup.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        // POST api/values
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserModel user)
        {
            user.Username = user.Username.ToLower();
            if (await _repo.UserExists(user.Username)) return BadRequest("Username already exixts");

            var userToCreate = new User
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            var createdUser = await _repo.Register(userToCreate, user.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel user)
        {
            //always attempt to login with lowercase username
            var userFromDb = await _repo.Login(user.Username.ToLower(), user.Password);
            if (userFromDb == null) return Unauthorized();

            //Add two claims the users ID and their Username
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userFromDb.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromDb.Username)
            };

            //Create the Security Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value)); 

            //To ensure that the token is a valid token when it comes back the server needs to sign the token
            //Encrypt key with a hashing algorithm 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //Create token by passing claims as the subject
            //Expiry date in this case is 1 day (24 hrs) 
            //Assign the signing credentials
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesc);
            //Write the token into the response
            return Ok(new { token = tokenHandler.WriteToken(token)});
        }
    }
}
