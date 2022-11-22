using ApiClient.Repositories;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using TeendokLista.MAUI.Services;

namespace TeendokLista.MAUI.Repositories.API
{
    public class FelhasznaloAPIRepository : BaseAPIRepository, IFelhasznaloRepository
    {
        public FelhasznaloAPIRepository(string path, string baseUrl = null) : base(baseUrl, path)
        {

        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var request = await client.PostAsJsonAsync(_path + "/login", new { UserName = username, Password = password });
            // sikeres státuszkód esetén
            if (request.IsSuccessStatusCode)
            {
                var response = await request.Content.ReadFromJsonAsync<JsonObject>();

                CurrentUser.Id = int.Parse(response.FirstOrDefault(x => x.Key == "id").Value.ToString());
                CurrentUser.FelhasznaloNev = response.FirstOrDefault(x => x.Key == "felhasznaloNev").Value.ToString();
                CurrentUser.Szerepkor = response.FirstOrDefault(x => x.Key == "szerepkor").Value.ToString();
                CurrentUser.Access_Token = response.FirstOrDefault(x => x.Key == "access_Token").Value.ToString();
                CurrentUser.Refresh_Token = response.FirstOrDefault(x => x.Key == "refresh_Token").Value.ToString();

                return "Sikeres bejelentkezés.";
            }

            return "Hibás felhasználónév vagy jelszó.";
        }
    }

}
