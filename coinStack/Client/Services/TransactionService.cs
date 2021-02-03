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

        public event Action OnChange;
        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();
        public IList<PortfolioTransaction> PortfolioTransactions { get; set; } = new List<PortfolioTransaction>();

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

        public async Task LoadPortfolioTransactions()
        {
            PortfolioTransactions.Clear();
            PortfolioTransactions = await _http.GetFromJsonAsync<IList<PortfolioTransaction>>("api/portfoliotransaction");
        }

        public async Task LoadTransactions()
        {
            if (Transactions.Count != PortfolioTransactions.Count)
            {
                foreach (PortfolioTransaction p in PortfolioTransactions)
                {
                    var transaction = await _http.GetFromJsonAsync<Transaction>($"api/transaction/{p.Id}");
                    Transactions.Add(transaction);
                }
            }
        }

        public async Task DeleteTransaction(Transaction t)
        {
            var result = await _http.PostAsJsonAsync<Transaction>("api/transaction/deletetransaction", t);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _toastService.ShowError(await result.Content.ReadAsStringAsync());
            }
            else
            {
                _toastService.ShowSuccess("The transaction was deleted from your portfolio", "Transaction deleted");
            }
        }

        void TransactionsChanged() => OnChange.Invoke();
    }
}
