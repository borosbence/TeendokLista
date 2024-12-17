namespace TeendokLista.MAUI.Services
{
    public static class CurrentUser
    {
        public static int Id { get; set; }
        public static string? Felhasznalonev { get; set; }
        public static string? Szerepkor { get; set; }

        // Ez az értékéket TokenAuthHandler már nem használja
        public static string? AccessToken { get; set; }
        public static string? RefreshToken { get; set; }
    }
}