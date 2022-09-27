using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TeendokLista.MAUI.Models;
using TeendokLista.MAUI.Services;

namespace TeendokLista.MAUI.Handlers
{
    public class TokenAuthHandler : DelegatingHandler
    {
        public TokenAuthHandler()
        {
            InnerHandler = new HttpClientHandler();
        }
        private const string BASE_URL = "http://localhost:5000/";
        private const string REFRESH_TOKEN_PATH = BASE_URL + "api/token/refresh";
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUser.Access_Token);
            var response = await base.SendAsync(request, cancellationToken);

            // 401-es Státuszkódra való tesztelés
            if (response.StatusCode == HttpStatusCode.Unauthorized && request.Headers.Contains("Authorization"))
            {
                // Küld egy új kérést, hogy megkapja a tokent
                var refreshReqMessage = new HttpRequestMessage(HttpMethod.Post, REFRESH_TOKEN_PATH);
                refreshReqMessage.Content = new StringContent(JsonSerializer.Serialize(new JwtToken()), Encoding.UTF8, "application/json");

                var refreshRequest = await base.SendAsync(refreshReqMessage, cancellationToken);
                var jwtToken = await refreshRequest.Content.ReadFromJsonAsync<JwtToken>();
                CurrentUser.Access_Token = jwtToken.Access_Token;
                CurrentUser.Refresh_Token = jwtToken.Refresh_Token;
                request.Headers.Remove("Authorization");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.Access_Token);

                // Kérés újrapróbálása az új tokenekkel
                response = await base.SendAsync(request, cancellationToken);
            }

            return response;
        }
    }
}
