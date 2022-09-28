using ApiClient.Models;

namespace TeendokLista.MAUI.Repositories.Local
{
    public class FelhasznaloLocalRepository : IFelhasznaloRepository
    {
        public Task<string> AuthenticateAsync(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                LoggedUser.Current.Id = 1;
                LoggedUser.Current.FelhasznaloNev = "admin";
                LoggedUser.Current.Szerepkor = "admin";
                return Task.FromResult("Sikeres bejelentkezés.");
            }
            return Task.FromResult("Hibás felhasználónév vagy jelszó.");
        }
    }
}
