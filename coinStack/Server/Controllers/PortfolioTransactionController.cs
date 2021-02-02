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
    public class PortfolioTransactionController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;
        public PortfolioTransactionController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPortfolioTransaction([FromBody] Transaction transaction)
        {
            var coin = _context.Coins.Find(transaction.Coinid);
            if (coin == null)
            {
                return BadRequest("This coin was not found in the database. Please add it to your portfolio and try again.");
            }
            var user = await _utilityService.GetUser();
            var portfolio = await _context.UserPortfolios.FirstOrDefaultAsync<UserPortfolio>(u => u.UserId == user.Id && u.CurrentlySelected == true);

            if (!await _context.PortfolioCoins.AnyAsync<PortfolioCoin>(p => p.UserPortfolioId == portfolio.Id && p.Coinid == coin.id))
            {
                return BadRequest("Please add this coin to your portfolio first and then create the transactions for it.");
            }

            await _context.Transactions.AddAsync(transaction);
            _context.SaveChanges();

            PortfolioTransaction portfolioTransaction = new PortfolioTransaction()
            {
                UserPortfolioId = portfolio.Id,
                TransactionId = transaction.Id
            };

            await _context.PortfolioTransactions.AddAsync(portfolioTransaction);
            _context.SaveChanges();

            return Ok(portfolioTransaction);
        }
    }
}
