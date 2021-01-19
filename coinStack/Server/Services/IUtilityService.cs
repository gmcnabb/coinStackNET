using coinStack.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coinStack.Server.Services
{
    public interface IUtilityService
    {
        Task<User> GetUser();
        Task<UserWatchlist> GetUserWatchlist();
        Task<List<UserWatchlist>> GetUserWatchlists();
    }
}
