using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeendokLista.MAUI.Repositories
{
    public interface IFelhasznaloRepository
    {
        string Authenticate(string username, string password);
    }
}
