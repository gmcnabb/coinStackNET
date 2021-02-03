using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coinStack.Shared;

namespace coinStack.Client.Services
{
    public interface ITransactionService
    {
        event Action OnChange;
        IList<Transaction> Transactions { get; set; }
        IList<PortfolioTransaction> PortfolioTransactions { get; set; }
        Task AddPortfolioTransaction(Transaction transaction);
        Task LoadPortfolioTransactions();
        Task LoadTransactions();
        Task DeleteTransaction(Transaction t);
    }
}
