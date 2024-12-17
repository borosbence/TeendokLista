namespace TeendokLista.MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // Téma kényszerítése
            Application.Current!.UserAppTheme = AppTheme.Light;
            MainPage = new AppShell();
        }
    }
}