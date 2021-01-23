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

        void AddCoin(string CoinId);
        Task LoadCoinsAsync();
    }
}
