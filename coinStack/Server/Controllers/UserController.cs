using coinStack.Server.Data;
using coinStack.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace coinStack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }

        private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        private async Task<User> GetUser() => await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

        [HttpGet("GetWatchlists")]
        public async Task<IActionResult> GetWatchlists()
        {
            var user = await GetUser();
            return Ok(user.Watchlists);
        }

    }
}
