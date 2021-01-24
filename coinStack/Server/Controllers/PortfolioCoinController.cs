using coinStack.Server.Data;
using coinStack.Server.Services;
using coinStack.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace coinStack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioCoinController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public PortfolioCoinController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        [HttpPost]
        public async Task<IActionResult> BuildPortfolioCoin([FromBody] string coinId)
        {
            var coin = _context.Coins.Find(coinId);
            var user = await _utilityService.GetUser();
            var portfolio = await _context.UserPortfolios.FirstOrDefaultAsync<UserPortfolio>(u => u.UserId == user.Id && u.CurrentlySelected == true);

            if (await _context.PortfolioCoins.AnyAsync<PortfolioCoin>(p => p.UserPortfolioId == portfolio.Id && p.Coinid == coin.id))
            {
                return BadRequest("The provided coin is already associated with this portfolio.");
            }

            PortfolioCoin newPortfolioCoin = new PortfolioCoin
            {
                UserPortfolioId = portfolio.Id,
                Coinid = coin.id
            };

            await _context.PortfolioCoins.AddAsync(newPortfolioCoin);
            await _context.SaveChangesAsync();
            return Ok(newPortfolioCoin);
        }

        [HttpGet]
        public async Task<IActionResult> LoadPortfolioCoins()
        {
            var user = await _utilityService.GetUser();
            var portfolio = await _context.UserPortfolios.FirstOrDefaultAsync<UserPortfolio>(u => u.UserId == user.Id && u.CurrentlySelected == true);
            var portfolioCoins = await _context.PortfolioCoins.Where(p => p.UserPortfolioId == portfolio.Id).ToListAsync();

            if (portfolioCoins == null)
            {
                return BadRequest("No coins found for this portfolio.");
            }

            return Ok(portfolioCoins);
        }

    }
}
