namespace TeendokLista.MAUI.Repositories.Local
{
    public class FelhasznaloLocalRepository : IFelhasznaloRepository
    {
        public string Authenticate(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                return "Sikeres bejelentkezés.";
            }
            return "Hibás felhasználónév vagy jelszó.";
        }
    }
}
