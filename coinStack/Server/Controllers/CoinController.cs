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

        [HttpGet]
        public async Task<IActionResult> GetCoins()
        {
            var coins = await _context.Coins.ToListAsync();
            return Ok(coins);
        }

        [HttpPost]
        public async Task<IActionResult> AddCoin(Coin coin)
        {
            await _context.Coins.AddAsync(coin);
            await _context.SaveChangesAsync();
            return Ok(await _context.Coins.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoin(int id, Coin coin)
        {
            Coin dbCoin = await _context.Coins.FirstOrDefaultAsync(c => c.Id == id);
            if (dbCoin == null)
            {
                return NotFound("Coin with given id not found");
            }

            dbCoin.GeckoId = coin.GeckoId;
            dbCoin.GeckoName = coin.GeckoName;
            dbCoin.GeckoSymbol = coin.GeckoSymbol;

            await _context.SaveChangesAsync();

            return Ok(dbCoin);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoin(int id)
        {
            Coin dbCoin = await _context.Coins.FirstOrDefaultAsync(c => c.Id == id);
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
