using ApiClient.Models;
using ApiClient.Repositories;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

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

                LoggedUser.Current.Id = int.Parse(response.FirstOrDefault(x => x.Key == "id").Value.ToString());
                LoggedUser.Current.FelhasznaloNev = response.FirstOrDefault(x => x.Key == "felhasznaloNev").Value.ToString();
                LoggedUser.Current.Szerepkor = response.FirstOrDefault(x => x.Key == "szerepkor").Value.ToString();
                LoggedUser.Current.Access_Token = response.FirstOrDefault(x => x.Key == "access_Token").Value.ToString();
                LoggedUser.Current.Refresh_Token = response.FirstOrDefault(x => x.Key == "refresh_Token").Value.ToString();

                return "Sikeres bejelentkezés.";
            }

            return "Hibás felhasználónév vagy jelszó.";
        }

        // TODO: Logout
    }

}
