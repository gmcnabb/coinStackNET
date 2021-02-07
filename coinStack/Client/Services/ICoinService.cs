using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coinStack.Shared;

namespace coinStack.Client.Services
{
    public interface ICoinService
    {
        IList<Coin> Coins { get; set; }
        IList<PortfolioCoin> PortfolioCoins { get; set; }
        Task<bool> CheckForCoin(string coinId);
        Task<bool> AddCoin(Coin coin);
        Task AddPortfolioCoin(Coin c);
        Task RemovePortfolioCoin(string coinId);
    }
}
