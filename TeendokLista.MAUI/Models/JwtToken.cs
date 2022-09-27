using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeendokLista.MAUI.Services;

namespace TeendokLista.MAUI.Models
{
    // TODO: nem szükséges
    public class JwtToken
    {
        public JwtToken()
        {
            Access_Token = CurrentUser.Access_Token;
            Refresh_Token = CurrentUser.Refresh_Token;
        }
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
    }
}
