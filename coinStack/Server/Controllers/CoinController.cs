using coinStack.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coinStack.Shared;

namespace coinStack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinController : ControllerBase
    {
        private readonly DataContext _context;
        public CoinController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetCoin(string id)
        {
            var coin = _context.Coins.Find(id);
            return Ok(coin);
        }

        [HttpGet("GetAllCoins")]
        public async Task<IActionResult> GetAllCoins()
        {
            var coins = await _context.Coins.ToListAsync();
            return Ok(coins);
        }

        [HttpGet("CheckForCoin/{id}")]
        public IActionResult CheckForCoin(string id)
        {
            bool exists = _context.Coins.Any(c => c.id == id);
            return Ok(exists);
        }

        [HttpPost("AddCoin")]
        public async Task<IActionResult> AddCoin([FromBody] Coin coin)
        {
            if (_context.Coins.Any(c => c.id == coin.id))
            {
                return BadRequest("This coin already exists in the database");
            }
            await _context.Coins.AddAsync(coin);
            await _context.SaveChangesAsync();
            return Ok(_context.Coins.Find(coin.id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoin(string id, Coin coin)
        {
            Coin dbCoin = await _context.Coins.FirstOrDefaultAsync(c => c.id == id);
            if (dbCoin == null)
            {
                return NotFound("Coin with given id not found");
            }

            dbCoin.id = coin.id;
            dbCoin.name = coin.name;
            dbCoin.symbol = coin.symbol;

            await _context.SaveChangesAsync();

            return Ok(dbCoin);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoin(string id)
        {
            Coin dbCoin = await _context.Coins.FirstOrDefaultAsync(c => c.id == id);
            if (dbCoin == null)
            {
                return NotFound("Coin with given id not found");
            }


            _context.Coins.Remove(dbCoin);

            await _context.SaveChangesAsync();

            return Ok(await _context.Coins.ToListAsync());
        }

    }
}
