using coinStack.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace coinStack.Client.Services
{
    public class CoinService : ICoinService
    {
        public event Action OnChange;
        private readonly HttpClient _http;
        public CoinService(HttpClient http)
        {
            _http = http;
        }

        public IList<Coin> Coins { get; set; } = new List<Coin>();
        public IList<PortfolioCoin> PortfolioCoins { get; set; } = new List<PortfolioCoin>();
        void CoinsChanged() => OnChange.Invoke();

        public async Task<bool> CheckForCoin(string coinId)
        {
            var result = await _http.GetFromJsonAsync<bool>($"api/coin/checkforcoin/{coinId}");
            return result;
        }

        public async Task<ServiceResponse<Coin>> AddCoin(Coin coin)
        {
            var result = await _http.PostAsJsonAsync<Coin>("api/coin/addcoin", coin);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<Coin>>();
        }


        public async Task LoadCoinsAsync()
        {
            if (PortfolioCoins.Count != Coins.Count)
            {
                Coins.Clear();
                foreach (PortfolioCoin p in PortfolioCoins)
                {
                    var coin = await _http.GetFromJsonAsync<Coin>($"api/coin/{p.Coinid}");
                    Coins.Add(coin);
                }
            }
        }

        public async Task LoadPortfolioCoinsAsync()
        {
            PortfolioCoins.Clear();
            PortfolioCoins = await _http.GetFromJsonAsync<IList<PortfolioCoin>>("api/portfoliocoin");
        }

        public async Task<ServiceResponse<PortfolioCoin>> AddPortfolioCoin(string coinId)
        {
            var result = await _http.PostAsJsonAsync<string>("api/portfoliocoin", coinId);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<PortfolioCoin>>();
        }
    }
}
