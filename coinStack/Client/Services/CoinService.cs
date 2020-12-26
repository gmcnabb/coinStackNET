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
        public CoinService(HttpClient http)
        {
            _http = http;
        }

        public IList<Coin> Coins { get; set; } = new List<Coin>();

        public void AddCoin(int CoinId)
        {
            Coin coin = Coins.First(coin => coin.CoinId == CoinId);

        }

        public async Task LoadCoinsAsync()
        {
            if (Coins.Count == 0)
            {
                Coins = await _http.GetFromJsonAsync<IList<Coin>>("api/coin");
            }
        }
    }
}
