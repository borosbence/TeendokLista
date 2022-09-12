using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeendokLista.MAUI.Repositories
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
