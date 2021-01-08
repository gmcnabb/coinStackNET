﻿using coinStack.Server.Data;
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

        public UserWatchlistController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        [HttpPost]
        public async Task<IActionResult> BuildUserWatchlist([FromBody] int watchlistId)
        {
            var watchlist = await _context.UserWatchlists.FirstOrDefaultAsync<UserWatchlist>(u => u.Id == watchlistId);
            var user = await _utilityService.GetUser();

            UserWatchlist newUserWatchlist = new UserWatchlist
            {
                WatchlistId = watchlistId,
                UserId = user.Id
            };

            await _context.UserWatchlists.AddAsync(newUserWatchlist);
            await _context.SaveChangesAsync();
            return Ok(newUserWatchlist);

        }
    }
}