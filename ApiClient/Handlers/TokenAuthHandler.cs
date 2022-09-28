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

        public TokenAuthHandler(string path)
        {
            _path = path;
            InnerHandler = new HttpClientHandler();
        }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Ha nem tartalmazza a hitelesítési fejlécet, akkor adja hozzá
            if (!request.Headers.Contains("Bearer"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LoggedUser.Current.Access_Token);
            }
            var response = await base.SendAsync(request, cancellationToken);

            // Ha válaszul 401-es státuszkódot kap
            if (response.StatusCode == HttpStatusCode.Unauthorized && request.Headers.Contains("Bearer"))
            {
                // Küld egy új kérést, hogy megkapja a tokent
                var refreshReqMessage = new HttpRequestMessage(HttpMethod.Post, _path);
                refreshReqMessage.Content = new StringContent(JsonSerializer.Serialize(new JwtToken()), Encoding.UTF8, "application/json");

                // Token válasz a szervertől
                var refreshRequest = await base.SendAsync(refreshReqMessage, cancellationToken);
                var jwtToken = await refreshRequest.Content.ReadFromJsonAsync<JwtToken>();

                LoggedUser.Current.Access_Token = jwtToken.Access_Token;
                LoggedUser.Current.Refresh_Token = jwtToken.Refresh_Token;

                // Jelenlegi fejléc cseréje
                request.Headers.Remove("Authorization");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.Access_Token);

                // Kérés újrapróbálása az új tokenekkel
                response = await base.SendAsync(request, cancellationToken);
            }

            return response;
        }
    }
}
