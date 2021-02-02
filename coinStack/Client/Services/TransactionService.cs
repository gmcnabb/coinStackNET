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
    public class TransactionService : ITransactionService
    {
        private readonly HttpClient _http;
        private readonly IToastService _toastService;
        public TransactionService(HttpClient http, IToastService toastService)
        {
            _http = http;
            _toastService = toastService;
        }
        public async Task AddPortfolioTransaction(Transaction transaction)
        {
            var result = await _http.PostAsJsonAsync<Transaction>("api/portfoliotransaction/", transaction);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _toastService.ShowError(await result.Content.ReadAsStringAsync());
            }
            else
            {
                _toastService.ShowSuccess("The transaction was added to your account!", "Transaction added!");
            }
        }
    }
}
