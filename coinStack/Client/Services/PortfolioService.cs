using coinStack.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace coinStack.Client.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly HttpClient _http;
        public PortfolioService(HttpClient http)
        {
            _http = http;
        }

        public event Action OnChange;
        public List<UserPortfolio> Portfolios { get; set; }

        public async Task GetPortfolios()
        {
            Portfolios = await _http.GetFromJsonAsync<List<UserPortfolio>>("api/userportfolio/getportfolios");

        }
        public async Task<ServiceResponse<int>> ChangePortfolio(int portfolioId)
        {
            var result = await _http.PostAsJsonAsync<int>("api/userportfolio/changeportfolio", portfolioId);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

        void PortfoliosChanged() => OnChange.Invoke();

    }
}
