﻿using ApiClient.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ApiClient.Handlers
{
    public class TokenAuthHandler : DelegatingHandler
    {
        private readonly string _path;
        private readonly string? _baseUrl;
        private string _accessToken;
        private string _refreshToken;

        public TokenAuthHandler(string accessToken, string refreshToken, string path = "api/token/refresh", string baseUrl = "http://localhost:5000/")
        {
            _accessToken = accessToken;
            _refreshToken = refreshToken;
            _path = path;
            _baseUrl = baseUrl;
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
            if (response.StatusCode == HttpStatusCode.Unauthorized && request.Headers.Authorization != null)
            {
                // Küld egy új kérést, hogy megkapja a tokent
                HttpRequestMessage refreshReqMessage = new(HttpMethod.Post, _baseUrl + _path);
                JwtModel oldToken = new(_accessToken, _refreshToken);
                refreshReqMessage.Content = new StringContent(JsonSerializer.Serialize(oldToken), Encoding.UTF8, "application/json");

                // Token válasz a szervertől
                var refreshRequest = await base.SendAsync(refreshReqMessage, cancellationToken);
                JwtModel? jwtToken = await refreshRequest.Content.ReadFromJsonAsync<JwtModel>();

                if (jwtToken != null)
                {
                    // TODO: Ha vissza kell adni a tokeneket, akkor ezt kell tovább fejleszteni
                    //_accessToken = jwtToken.AccessToken;
                    //_refreshToken = jwtToken.RefreshToken;

                    // Jelenlegi fejléc cseréje
                    request.Headers.Remove("Authorization");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.AccessToken);
                }

                // Kérés újrapróbálása az új tokenekkel
                response = await base.SendAsync(request, cancellationToken);
            }

            return response;
        }
    }
}
