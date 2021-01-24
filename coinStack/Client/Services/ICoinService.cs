using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coinStack.Shared;

namespace coinStack.Client.Services
{
    public interface ICoinService
    {
        event Action OnChange;
        IList<Coin> Coins { get; set; }
        IList<PortfolioCoin> PortfolioCoins { get; set; }
        Task<bool> CheckForCoin(string coinId);
        Task<ServiceResponse<Coin>> AddCoin(Coin coin);
        Task LoadCoinsAsync();
        Task LoadPortfolioCoinsAsync();
        Task<ServiceResponse<PortfolioCoin>> AddPortfolioCoin(string coinId);
    }
}
