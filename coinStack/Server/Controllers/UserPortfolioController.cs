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
    public class UserPortfolioController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public UserPortfolioController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        [HttpGet("GetPortfolios")]
        public async Task<IActionResult> GetPortfolios()
        {
            var user = await _utilityService.GetUser();
            var portfolios = await _context.UserPortfolios.Where(u => u.UserId == user.Id).ToListAsync();
            if (portfolios == null)
            {
                return BadRequest("No portfolios found for this user.");
            }
            return Ok(portfolios);
        }

        [HttpPost]
        public async Task<IActionResult> BuildUserPortfolio([FromBody] int portfolioId)
        {
            var portfolio = await _context.UserPortfolios.FirstOrDefaultAsync<UserPortfolio>(u => u.Id == portfolioId);
            var user = await _utilityService.GetUser();

            UserPortfolio newUserPortfolio = new UserPortfolio
            {
                Id = portfolioId,
                UserId = user.Id
            };

            await _context.UserPortfolios.AddAsync(newUserPortfolio);
            await _context.SaveChangesAsync();
            return Ok(newUserPortfolio);

        }

        [HttpPost("ChangePortfolio")]
        public async Task<IActionResult> ChangePortfolio([FromBody] int portfolioId)
        {
            var user = _utilityService.GetUser();
            var portfolio = await _context.UserPortfolios.FirstOrDefaultAsync<UserPortfolio>(u => u.UserId == user.Id && u.CurrentlySelected == true);
            var newPortfolio = await _context.UserPortfolios.FirstOrDefaultAsync<UserPortfolio>(u => u.Id == portfolioId && u.UserId == user.Id);

            if (portfolio == null || newPortfolio == null)
            {
                return BadRequest("Selected portfolios not found for this user.");
            }

            portfolio.CurrentlySelected = false;
            newPortfolio.CurrentlySelected = true;
            await _context.SaveChangesAsync();

            return Ok(newPortfolio);
        }
    }
}