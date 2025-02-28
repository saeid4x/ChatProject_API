using AutoMapper;
using ChatProject.API.DTOs;
using ChatProject.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AccountController
            (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
             IConfiguration config,
             IMapper mapper
            )
        {
            _userManager = userManager;
            _signInManager=signInManager;
            _config=config;
            _mapper=mapper;
        }


        // POST: api/auth/register 
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = "User registered successfully" });
        }


        // POST: api/account/login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userExist = await _userManager.FindByNameAsync(model.Username);
            if(userExist == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(userExist, model.Password,false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }


            // Generate JWT token
            var token = GenerateJwtToken(userExist);
            return Ok(new { Token = token });
        }


        private string GenerateJwtToken(ApplicationUser user)
        {
            var authClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                 new Claim("username", user.UserName)  // Custom claim
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                claims: authClaims,
                expires: DateTime.UtcNow.AddHours(16),
                signingCredentials: creds

                );

            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
    }
}
