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
        private readonly IUser _user;

        // TODO: user, mint referenciaként?
        public TokenAuthHandler(string path, IUser user)
        {
            _path = path;
            _user = user;
            InnerHandler = new HttpClientHandler();
        }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Ha nem tartalmazza a hitelesítési fejlécet, akkor adja hozzá
            if (!request.Headers.Contains("Bearer"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _user.Access_Token);
            }
            var response = await base.SendAsync(request, cancellationToken);

            // Ha válaszul 401-es státuszkódot kap
            if (response.StatusCode == HttpStatusCode.Unauthorized && request.Headers.Contains("Bearer"))
            {
                // Küld egy új kérést, hogy megkapja a tokent
                var refreshReqMessage = new HttpRequestMessage(HttpMethod.Post, _path);
                var oldToken = new JwtToken(_user.Access_Token, _user.Refresh_Token);
                refreshReqMessage.Content = new StringContent(JsonSerializer.Serialize(oldToken), Encoding.UTF8, "application/json");

                // Token válasz a szervertől
                var refreshRequest = await base.SendAsync(refreshReqMessage, cancellationToken);
                var jwtToken = await refreshRequest.Content.ReadFromJsonAsync<JwtToken>();

                if (jwtToken != null)
                {
                    _user.Access_Token = jwtToken.Access_Token;
                    _user.Refresh_Token = jwtToken.Refresh_Token;

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
