using ApiClient.Repositories;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using TeendokLista.MAUI.Models;

namespace TeendokLista.MAUI.Repositories.API
{
    public class FelhasznaloAPIRepository : BaseAPIRepository, IFelhasznaloRepository
    {
        private CurrentUser _currentUser;
        public FelhasznaloAPIRepository(CurrentUser currentuser, string path, string? baseUrl = null) : base(baseUrl, path)
        {
            _currentUser = currentuser;
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var response = await client.PostAsJsonAsync(_path + "/login", new { Username = username, Password = password }); 
            // Sikeres státuszkód esetén
            if (response.IsSuccessStatusCode)
            {
                // Nincs megadva pontosan, hogy milyen objektummá alakítja a JSON választ
                var data = await response.Content.ReadFromJsonAsync<CurrentUser>();
                if (data != null)
                { 
                    _currentUser = data;
                }
                // var data = await response.Content.ReadFromJsonAsync<JsonObject>();
                //if (data != null)
                //{
                //    _currentUser.Id = int.Parse(data.FirstOrDefault(x => x.Key == "id").Value.ToString());
                //    _currentUser.FelhasznaloNev = data.FirstOrDefault(x => x.Key == "felhasznaloNev").Value.ToString();
                //    _currentUser.Szerepkor = data.FirstOrDefault(x => x.Key == "szerepkor").Value.ToString();
                //    _currentUser.AccessToken = data.FirstOrDefault(x => x.Key == "accessToken").Value.ToString();
                //    _currentUser.RefreshToken = data.FirstOrDefault(x => x.Key == "refreshToken").Value.ToString();
                //}
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