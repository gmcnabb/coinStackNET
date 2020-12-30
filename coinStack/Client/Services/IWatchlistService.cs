using coinStack.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coinStack.Client.Services
{
    public interface IWatchlistService
    {
        event Action OnChange;
        List<Watchlist> Watchlists { get; set; }
        Task GetWatchlists();
    }
}
