using ApiClient.Repositories;
using System.Net;
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
            var response = await client.PostAsJsonAsync(_path + "/login", new { UserName = username, Password = password }); 
            // Sikeres státuszkód esetén
            if (response.IsSuccessStatusCode)
            {
                // Nincs megadva pontosan, hogy milyen objektummá alakítja a JSON választ
                var data = await response.Content.ReadFromJsonAsync<JsonObject>();

                CurrentUser.Id = int.Parse(data.FirstOrDefault(x => x.Key == "id").Value.ToString());
                CurrentUser.FelhasznaloNev = data.FirstOrDefault(x => x.Key == "felhasznaloNev").Value.ToString();
                CurrentUser.Szerepkor = data.FirstOrDefault(x => x.Key == "szerepkor").Value.ToString();
                CurrentUser.Access_Token = data.FirstOrDefault(x => x.Key == "access_Token").Value.ToString();
                CurrentUser.Refresh_Token = data.FirstOrDefault(x => x.Key == "refresh_Token").Value.ToString();

                return "Sikeres bejelentkezés.";
            }
            // hibakeresés
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return "Hibás felhasználónév vagy jelszó.";
            }
            else
            {
                return "Váratlan hiba történt";
            }
        }
    }
}