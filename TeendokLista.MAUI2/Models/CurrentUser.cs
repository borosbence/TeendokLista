namespace TeendokLista.MAUI.Models
{
    public class CurrentUser
    {
        public int Id { get; set; }
        public string? FelhasznaloNev { get; set; }
        public string? Szerepkor { get; set; }

        // Ez az értékéket TokenAuthHandler már nem használja
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}