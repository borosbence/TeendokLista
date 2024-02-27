namespace TeendokLista.API.Models
{
    public partial class LoginToken
    {
        public LoginToken(string token, int felhasznalo_id)
        {
            this.token = token;
            this.felhasznalo_id = felhasznalo_id;
            lejarat_datum = DateTime.Now.AddDays(7);
        }
    }
}
