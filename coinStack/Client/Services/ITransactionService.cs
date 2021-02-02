using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coinStack.Shared;

namespace coinStack.Client.Services
{
    public interface ITransactionService
    {
        Task AddPortfolioTransaction(Transaction transaction);
    }
}
