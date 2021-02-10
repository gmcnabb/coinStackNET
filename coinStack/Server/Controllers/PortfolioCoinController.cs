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
        public async Task<IActionResult> BuildPortfolioCoin([FromBody] Coin c)
        {
            var coin = _context.Coins.Find(c.id);
            var user = await _utilityService.GetUser();
            var portfolio = await _context.UserPortfolios.FirstOrDefaultAsync<UserPortfolio>(u => u.UserId == user.Id && u.CurrentlySelected == true);

            if (await _context.PortfolioCoins.AnyAsync<PortfolioCoin>(p => p.UserPortfolioId == portfolio.Id && p.Coinid == coin.id))
            {
                return BadRequest("That coin is already associated with this portfolio.");
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

        [HttpPost("Remove")]
        public async Task<IActionResult> RemovePortfolioCoin([FromBody] string coinId)
        {
            var coin = _context.Coins.Find(coinId);
            var user = await _utilityService.GetUser();
            var portfolio = await _context.UserPortfolios.FirstOrDefaultAsync<UserPortfolio>(u => u.UserId == user.Id && u.CurrentlySelected == true);

            var portfolioCoin = await _context.PortfolioCoins.FirstOrDefaultAsync<PortfolioCoin>(p => p.UserPortfolioId == portfolio.Id && p.Coinid == coin.id);
            if (portfolioCoin == null)
            {
                return BadRequest($"{coin.name} not found in user's currently selected portfolio.");
            }

            var portfolioTransactions = await _context.PortfolioTransactions.Where(p => p.UserPortfolioId == portfolio.Id).ToListAsync();
            var transactions = from p in portfolioTransactions
                               join t in _context.Transactions on p.TransactionId equals t.Id
                               select t;
            if (transactions.Any(t => t.Coinid == coinId))
            {
                return BadRequest($"Please delete your {coin.name} transactions before removing it from your portfolio.");
            }

            _context.PortfolioCoins.Remove(portfolioCoin);
            await _context.SaveChangesAsync();
            return Ok(portfolioCoin);
        }

    }
}
