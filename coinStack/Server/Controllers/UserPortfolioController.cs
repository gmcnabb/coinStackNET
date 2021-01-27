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

        [HttpPost("UpdatePortfolio")]
        public async Task<IActionResult> UpdatePortfolio([FromBody] UserPortfolio portfolio)
        {
            var user = await _utilityService.GetUser();
            var originalPortfolio = await _context.UserPortfolios.FirstOrDefaultAsync<UserPortfolio>(u => u.Id == portfolio.Id && u.UserId == user.Id);

            if (originalPortfolio == null)
            {
                return BadRequest($"Portfolio with id: {portfolio.Id} not found for user with id: {user.Id}.");
            }

            var currentlySelectedPortfolio = await _context.UserPortfolios.FirstOrDefaultAsync(u => u.CurrentlySelected == true);
            if (portfolio.CurrentlySelected && currentlySelectedPortfolio != null)
            {
                currentlySelectedPortfolio.CurrentlySelected = false;
            }

            originalPortfolio.Name = portfolio.Name;
            originalPortfolio.CurrentlySelected = portfolio.CurrentlySelected;
            await _context.SaveChangesAsync();
            return Ok(originalPortfolio);
        }

        [HttpPost("AddPortfolio")]
        public async Task<IActionResult> AddPortfolio([FromBody] UserPortfolio portfolio)
        {
            var user = await _utilityService.GetUser();
            portfolio.UserId = user.Id;
            portfolio.CurrentlySelected = false;

            await _context.AddAsync<UserPortfolio>(portfolio);
            await _context.SaveChangesAsync();
            return Ok(portfolio);
        }

        [HttpPost("DeletePortfolio")]
        public async Task<IActionResult> DeletePortfolio([FromBody] UserPortfolio portfolio)
        {
            var user = await _utilityService.GetUser();

            if (user.Id != portfolio.UserId)
            {
                return BadRequest("User does not own this portfolio");
            }

            _context.Remove(portfolio);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}