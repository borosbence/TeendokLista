namespace TeendokLista.MAUI.Models
{
    public class FelhasznaloModel
    {
        public int Id { get; set; }
        public string Felhasznalonev { get; set; } = null!;
        public string Szerepkor { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
