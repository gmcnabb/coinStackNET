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
        private readonly HttpClient _http;
        private readonly IToastService _toastService;
        public CoinService(HttpClient http, IToastService toastService)
        {
            _http = http;
            _toastService = toastService;
        }

        public IList<Coin> Coins { get; set; } = new List<Coin>();
        public IList<PortfolioCoin> PortfolioCoins { get; set; } = new List<PortfolioCoin>();

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

        public async Task RemovePortfolioCoin(string coinId)
        {
            var result = await _http.PostAsJsonAsync<string>("api/portfoliocoin/remove", coinId);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _toastService.ShowError(await result.Content.ReadAsStringAsync());
            }
            else
            {
                _toastService.ShowSuccess($"{coinId} was removed from your portfolio.", "Coin removed.");
            }
        }
    }
}
