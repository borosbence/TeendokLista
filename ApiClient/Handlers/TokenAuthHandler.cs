using ApiClient.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ApiClient.MAUI.Handlers
{
    public class TokenAuthHandler : DelegatingHandler
    {
        private readonly string _path = "api/token/refresh";
        private string _accessToken;
        private string _refreshToken;

        public TokenAuthHandler(string path, string accessToken, string refreshToken)
        {
            _path = path;
            _accessToken = accessToken;
            _refreshToken = refreshToken;
            InnerHandler = new HttpClientHandler();
        }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Ha nem tartalmazza a hitelesítési fejlécet, akkor adja hozzá
            if (!request.Headers.Contains("Bearer"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            }
            var response = await base.SendAsync(request, cancellationToken);

            // Ha válaszul 401-es státuszkódot kap
            if (response.StatusCode == HttpStatusCode.Unauthorized && request.Headers.Contains("Bearer"))
            {
                // Küld egy új kérést, hogy megkapja a tokent
                var refreshReqMessage = new HttpRequestMessage(HttpMethod.Post, _path);
                var oldToken = new JwtToken(_accessToken, _refreshToken);
                refreshReqMessage.Content = new StringContent(JsonSerializer.Serialize(oldToken), Encoding.UTF8, "application/json");

                // Token válasz a szervertől
                var refreshRequest = await base.SendAsync(refreshReqMessage, cancellationToken);
                var jwtToken = await refreshRequest.Content.ReadFromJsonAsync<JwtToken>();

                if (jwtToken != null)
                {
                    _accessToken = jwtToken.Access_Token;
                    _refreshToken = jwtToken.Refresh_Token;

                    // Jelenlegi fejléc cseréje
                    request.Headers.Remove("Authorization");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.Access_Token);
                }

                // Kérés újrapróbálása az új tokenekkel
                response = await base.SendAsync(request, cancellationToken);
            }

            return response;
        }
    }
}
