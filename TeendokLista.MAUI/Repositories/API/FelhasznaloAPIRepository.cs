using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TeendokLista.MAUI.Services;

namespace TeendokLista.MAUI.Repositories.API
{
    public class FelhasznaloAPIRepository : IFelhasznaloRepository
    {
        protected readonly HttpClient client;
        // TODO: App.xaml-be áthelyezni
        private const string BASE_URL = "http://localhost:5000/";
        // private const string BASE_URL = "http://192.168.1.11:5000/";
        private const string PATH = "api/token";

        public FelhasznaloAPIRepository()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri(BASE_URL)
            };
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var request = await client.PostAsJsonAsync(PATH + "/login", new { UserName = username, Password = password });
            // sikeres státuszkód esetén
            if (request.IsSuccessStatusCode)
            {
                var response = await request.Content.ReadFromJsonAsync<JsonObject>();

                CurrentUser.Id = int.Parse(response.FirstOrDefault(x => x.Key == "id").Value.ToString());
                CurrentUser.FelhasznaloNev = response.FirstOrDefault(x => x.Key == "felhasznaloNev").Value.ToString();
                CurrentUser.Szerepkor = response.FirstOrDefault(x => x.Key == "szerepkor").Value.ToString();
                CurrentUser.Access_Token = response.FirstOrDefault(x => x.Key == "access_Token").Value.ToString();
                CurrentUser.Refresh_Token = response.FirstOrDefault(x => x.Key == "refresh_Token").Value.ToString();


                // App.Current.Handler.MauiContext.Services.GetRequiredService<>

                return "Sikeres bejelentkezés.";
            }

            return "Hibás felhasználónév vagy jelszó.";
        }

        // TODO: Logout
    }
}
