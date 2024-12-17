using TeendokLista.MAUI.Services;

namespace TeendokLista.MAUI.Repositories.Local
{
    public class FelhasznaloLocalRepository : IFelhasznaloRepository
    {
        public Task<string> AuthenticateAsync(string username, string password)
        {
            // Tesztelés céljából
            if (username == "admin" && password == "admin")
            {
                CurrentUser.Id = 1;
                CurrentUser.Felhasznalonev = "admin";
                CurrentUser.Szerepkor = "admin";
                return Task.FromResult("Sikeres bejelentkezés.");
            }
            return Task.FromResult("Hibás felhasználónév vagy jelszó.");
        }
    }
}
