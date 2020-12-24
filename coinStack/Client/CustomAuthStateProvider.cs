using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Blazored.LocalStorage;

namespace coinStack.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;

        public CustomAuthStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var state = new AuthenticationState(new ClaimsPrincipal());

            if (await _localStorageService.GetItemAsync<bool>("isAuthed"))
            {
                var identity = new ClaimsIdentity(
                    new[]
                    {
                    new Claim(ClaimTypes.Name, "James")
                    }, "test auth type");

                var user = new ClaimsPrincipal(identity);
                state = new AuthenticationState(user);
            }

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }
    }
}
