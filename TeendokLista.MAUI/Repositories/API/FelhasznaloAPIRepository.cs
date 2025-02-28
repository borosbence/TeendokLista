using ApiClient.Repositories;
using System.Net;
using System.Net.Http.Json;
using TeendokLista.MAUI.Models;
using TeendokLista.MAUI.Services;

namespace TeendokLista.MAUI.Repositories.API
{
    public class FelhasznaloAPIRepository : BaseAPIRepository, IFelhasznaloRepository
    {
        public FelhasznaloAPIRepository(string path = "api/token/login", string? baseUrl = null) : base(path, baseUrl)
        {
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            // anoním objektum létrehozása
            var loginData = new { Username = username, Password = password };
            var response = await client.PostAsJsonAsync(_path, loginData); 
            // Sikeres státuszkód esetén
            if (response.IsSuccessStatusCode)
            {
                // Nincs megadva pontosan, hogy milyen objektummá alakítja a JSON választ
                var felhasznaloModel = await response.Content.ReadFromJsonAsync<FelhasznaloModel>();
                if (felhasznaloModel != null)
                {
                    CurrentUser.Id = felhasznaloModel.Id;
                    CurrentUser.Felhasznalonev = felhasznaloModel.Felhasznalonev;
                    CurrentUser.Szerepkor = felhasznaloModel.Szerepkor;
                    CurrentUser.AccessToken = felhasznaloModel.AccessToken;
                    CurrentUser.RefreshToken = felhasznaloModel.RefreshToken;
                }
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