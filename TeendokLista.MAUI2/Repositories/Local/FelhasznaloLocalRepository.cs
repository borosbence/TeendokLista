using TeendokLista.MAUI.Models;

namespace TeendokLista.MAUI.Repositories.Local
{
    public class FelhasznaloLocalRepository : IFelhasznaloRepository
    {
        private CurrentUser _currentUser;
        public FelhasznaloLocalRepository(CurrentUser currentuser)
        {
            _currentUser = currentuser;
        }
        public Task<string> AuthenticateAsync(string username, string password)
        {
            // Tesztelés céljából
            if (username == "admin" && password == "admin")
            {
                _currentUser.Id = 1;
                _currentUser.FelhasznaloNev = "admin";
                _currentUser.Szerepkor = "admin";
                return Task.FromResult("Sikeres bejelentkezés.");
            }
            return Task.FromResult("Hibás felhasználónév vagy jelszó.");
        }
    }
}
