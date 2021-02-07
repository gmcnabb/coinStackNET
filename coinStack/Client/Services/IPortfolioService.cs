using coinStack.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coinStack.Client.Services
{
    public interface IPortfolioService
    {
        event Action OnChange;
        IList<UserPortfolio> Portfolios { get; set; }
        Task GetPortfolios();
        Task UpdatePortfolio(UserPortfolio portfolio);
        Task AddPortfolio(UserPortfolio portfolio);
        Task DeletePortfolio(UserPortfolio portfolio);
    }
}
