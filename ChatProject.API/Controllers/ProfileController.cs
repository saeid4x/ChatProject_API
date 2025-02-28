using AutoMapper;
using ChatProject.API.DTOs;
using ChatProject.Domain.Entities;
using ChatProject.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Profile = ChatProject.Domain.Entities.Profile;

namespace ChatProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _mapper=mapper;
            _context = context;
            
        }

        // GET: api/profile
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null) return NotFound();

            return Ok(_mapper.Map<ProfileDto>(profile));
        }

        // PUT: api/profile
        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileDto model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null)
            {
                profile = _mapper.Map<Profile>(model);
                profile.UserId = userId;
                _context.Profiles.Add(profile);
            }
            else
            {
                _mapper.Map(model, profile);
            }

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Profile updated successfully" });

        }
    }
}
