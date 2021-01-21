using coinStack.Server.Data;
using coinStack.Server.Services;
using coinStack.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coinStack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWatchlistController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserWatchlistController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        [HttpGet("GetWatchlists")]
        public async Task<IActionResult> GetWatchlists()
        {
            var user = await _utilityService.GetUser();
            var watchlists = await _context.UserWatchlists.Where(u => u.UserId == user.Id).ToListAsync();
            if (watchlists == null)
            {
                return BadRequest("no watchlists found for this user");
            }
            return Ok(watchlists);
        }

        [HttpPost]
        public async Task<IActionResult> BuildUserWatchlist([FromBody] int watchlistId)
        {
            var watchlist = await _context.UserWatchlists.FirstOrDefaultAsync<UserWatchlist>(u => u.Id == watchlistId);
            var user = await _utilityService.GetUser();

            UserWatchlist newUserWatchlist = new UserWatchlist
            {
                Id = watchlistId,
                UserId = user.Id
            };

            await _context.UserWatchlists.AddAsync(newUserWatchlist);
            await _context.SaveChangesAsync();
            return Ok(newUserWatchlist);

        }

        [HttpPost("ChangeWatchlist")]
        public async Task<IActionResult> ChangeWatchlist([FromBody] int watchlistId)
        {
            var user = _utilityService.GetUser();
            var watchlist = await _context.UserWatchlists.FirstOrDefaultAsync<UserWatchlist>(u => u.UserId == user.Id && u.CurrentlySelected == true);
            var newWatchlist = await _context.UserWatchlists.FirstOrDefaultAsync<UserWatchlist>(u => u.Id == watchlistId && u.UserId == user.Id);

            if (watchlist == null || newWatchlist == null)
            {
                return BadRequest("selected watchlists not found for this user");
            }

            watchlist.CurrentlySelected = false;
            newWatchlist.CurrentlySelected = true;
            await _context.SaveChangesAsync();

            return Ok(newWatchlist);
        }
    }
}
