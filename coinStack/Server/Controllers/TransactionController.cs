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
    public class TransactionController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public TransactionController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        [HttpGet("{id}")]
        public IActionResult GetTransaction(int id)
        {
            var transaction = _context.Transactions.Find(id);
            return Ok(transaction);
        }

        [HttpPost("DeleteTransaction")]
        public async Task<IActionResult> DeleteTransaction([FromBody] Transaction t)
        {
            var user = await _utilityService.GetUser();
            var portfolio = await _context.UserPortfolios.FirstOrDefaultAsync(u => u.UserId == user.Id && u.CurrentlySelected == true);

            if (!await _context.PortfolioTransactions.AnyAsync(p => p.UserPortfolioId == portfolio.Id && p.TransactionId == t.Id))
            {
                return BadRequest("Transaction not found in user's currently selected portfolio.");
            }

            _context.Transactions.Remove(t);
            await _context.SaveChangesAsync();
            return Ok(t);
        }
    }
}
