using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using coinStack.Shared;

namespace coinStack.Client.Services
{
    public class PortfolioDataService
    {
        private readonly HttpClient _http;

        public PortfolioDataService(HttpClient http)
        {
            _http = http;
        }
        public event Action OnChange;
        void CalcsChanged() => OnChange.Invoke();
        public IList<PortfolioCoin> PortfolioCoins { get; set; } = new List<PortfolioCoin>();
        public IList<Coin> Coins { get; set; } = new List<Coin>();
        public IList<PortfolioTransaction> PortfolioTransactions { get; set; } = new List<PortfolioTransaction>();
        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();
        public IList<MarketDataResponse> MarketDataResponses { get; set; } = new List<MarketDataResponse>();
        public IList<CoinStats> PortfolioCoinStats { get; set; } = new List<CoinStats>();
        public double TotalValue { get; set; } = 0;
        public double TotalPnL { get; set; } = 0;
        public bool calculating { get; set; } = false;
        public bool initialized { get; set; } = false;

        public async Task LoadPortfolioCoins()
        {
            PortfolioCoins.Clear();
            PortfolioCoins = await _http.GetFromJsonAsync<IList<PortfolioCoin>>("api/portfoliocoin");
            CalcsChanged();
        }
        public async Task LoadCoins()
        {
            Coins.Clear();
            foreach (PortfolioCoin p in PortfolioCoins)
            {
                var coin = await _http.GetFromJsonAsync<Coin>($"api/coin/{p.Coinid}");
                Coins.Add(coin);
            }
            CalcsChanged();
        }
        public async Task LoadPortfolioTransactions()
        {
            PortfolioTransactions.Clear();
            PortfolioTransactions = await _http.GetFromJsonAsync<IList<PortfolioTransaction>>("api/portfoliotransaction");
            CalcsChanged();
        }
        public async Task LoadTransactions()
        {
            Transactions.Clear();
            foreach (PortfolioTransaction p in PortfolioTransactions)
            {
                var transaction = await _http.GetFromJsonAsync<Transaction>($"api/transaction/{p.Id}");
                Transactions.Add(transaction);
            }
            CalcsChanged();
        }
        public async Task GetPortfolioMarketData()
        {
            string coinIds = "";
            foreach (Coin c in Coins)
            {
                coinIds += (c.id + "%2C%20");
            }
            MarketDataResponses = await _http.GetFromJsonAsync<List<MarketDataResponse>>($"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&ids={coinIds}&order=market_cap_desc&per_page=250&page=1&sparkline=false");
            CalcsChanged();
        }
        public void CalculateAllCoinStats()
        {
            calculating = true;
            TotalValue = 0;
            TotalPnL = 0;
            PortfolioCoinStats.Clear();
            foreach (MarketDataResponse m in MarketDataResponses)
            {
                PortfolioCoinStats.Add(new CoinStats(m.id, m.current_price));
            }
            foreach (Transaction t in Transactions)
            {
                var coinStats = PortfolioCoinStats.First(c => c.coinId == t.Coinid);
                if (t.Type)
                {
                    coinStats.quantityBought += t.Quantity;
                    coinStats.sumOfCosts += (t.Quantity * t.USDValue);
                }
                if (!t.Type)
                {
                    coinStats.quantitySold += t.Quantity;
                    coinStats.sumOfProceeds += (t.Quantity * t.USDValue);
                }
            }
            foreach (CoinStats c in PortfolioCoinStats)
            {
                c.totalHoldings = c.quantityBought - c.quantitySold;
                c.netCost = c.sumOfCosts - c.sumOfProceeds;
                c.avgNetCost = c.totalHoldings > 0 ? c.netCost / c.totalHoldings : 0;
                c.marketValueHoldings = MarketDataResponses.First(m => m.id == c.coinId).current_price * c.totalHoldings;
                c.PnL = (c.marketValueHoldings + c.sumOfProceeds - c.sumOfCosts);
                c.percentChange = c.totalHoldings > 0 ? (c.PnL / c.netCost) * 100 : 0;
                TotalValue += c.marketValueHoldings;
                TotalPnL += c.PnL;
            }
            if (!initialized) { initialized = true; }
            calculating = false;
            CalcsChanged();
        }
    }
}
