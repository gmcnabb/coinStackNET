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
        List<UserPortfolio> Portfolios { get; set; }
        Task GetPortfolios();
        Task<ServiceResponse<UserPortfolio>> UpdatePortfolio(UserPortfolio portfolio);
    }
}
