﻿using coinStack.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace coinStack.Client.Services
{
    public class WatchlistService : IWatchlistService
    {
        private readonly HttpClient _http;

        public WatchlistService(HttpClient http)
        {
            _http = http;
        }

        public event Action OnChange;
        public List<UserWatchlist> Watchlists { get; set; } = null;


        public async Task GetWatchlists()
        {
            Watchlists = await _http.GetFromJsonAsync<List<UserWatchlist>>("api/userwatchlist/getwatchlists");
        }

        public async Task<ServiceResponse<int>> ChangeWatchlist(int watchlistId)
        {
            var result = await _http.PostAsJsonAsync<int>("api/userwatchlist/changewatchlist", watchlistId);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

        void WatchlistsChanged() => OnChange.Invoke();
    }
}