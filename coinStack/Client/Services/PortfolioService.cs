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
    public class PortfolioService : IPortfolioService
    {
        private readonly HttpClient _http;
        private readonly IToastService _toastService;
        public PortfolioService(HttpClient http, IToastService toastService)
        {
            _http = http;
            _toastService = toastService;
        }

        public event Action OnChange;
        public IList<UserPortfolio> Portfolios { get; set; }

        public async Task GetPortfolios()
        {
            Portfolios = await _http.GetFromJsonAsync<IList<UserPortfolio>>("api/userportfolio/getportfolios");
        }
        public async Task UpdatePortfolio(UserPortfolio portfolio)
        {
            var result = await _http.PostAsJsonAsync<UserPortfolio>("api/userportfolio/updateportfolio", portfolio);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _toastService.ShowError(await result.Content.ReadAsStringAsync());
            }
            else
            {
                _toastService.ShowSuccess($"{portfolio.Name} was updated successfully.", "Update successful");
            }
        }
        public async Task AddPortfolio(UserPortfolio portfolio)
        {
            var result = await _http.PostAsJsonAsync<UserPortfolio>("api/userportfolio/addportfolio", portfolio);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _toastService.ShowError(await result.Content.ReadAsStringAsync());
            }
            else
            {
                _toastService.ShowSuccess($"{portfolio.Name} was added to your account!", "Portfolio added!");
            }
        }

        public async Task DeletePortfolio(UserPortfolio portfolio)
        {
            var result = await _http.PostAsJsonAsync<UserPortfolio>("api/userportfolio/deleteportfolio", portfolio);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _toastService.ShowError(await result.Content.ReadAsStringAsync());
            }
            else
            {
                _toastService.ShowSuccess($"{portfolio.Name} has been deleted.", "Portfolio deleted");
            }
        }

        void PortfoliosChanged() => OnChange.Invoke();
    }
}
