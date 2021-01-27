using Blazored.Toast.Services;
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
        private readonly IToastService _toastService;
        public CoinService(HttpClient http, IToastService toastService)
        {
            _http = http;
            _toastService = toastService;
        }

        public IList<Coin> Coins { get; set; } = new List<Coin>();
        public IList<PortfolioCoin> PortfolioCoins { get; set; } = new List<PortfolioCoin>();
        void CoinsChanged() => OnChange.Invoke();

        public async Task<bool> CheckForCoin(string coinId)
        {
            var result = await _http.GetFromJsonAsync<bool>($"api/coin/checkforcoin/{coinId}");
            return result;
        }

        public async Task<bool> AddCoin(Coin coin)
        {
            var result = await _http.PostAsJsonAsync<Coin>("api/coin/addcoin", coin);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            }
            return true;
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

        public async Task AddPortfolioCoin(Coin c)
        {
            var result = await _http.PostAsJsonAsync<Coin>("api/portfoliocoin", c);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _toastService.ShowError(await result.Content.ReadAsStringAsync());
            }
            else
            {
                _toastService.ShowSuccess($"{c.name} was added to your portfolio!", "Coin added!");
            }
        }
    }
}
