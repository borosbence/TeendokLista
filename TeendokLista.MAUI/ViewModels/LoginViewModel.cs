using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TeendokLista.MAUI.Repositories;
using TeendokLista.MAUI.Views;

namespace TeendokLista.MAUI.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private IFelhasznaloRepository _repository;

        public LoginViewModel(IFelhasznaloRepository repository)
        {
            _repository = repository;
            // Akkor engedélyezze, ha nem üres a felhasználónév, jelszó
            LoginCommand = new AsyncRelayCommand(Login, CanLogin);
            // TODO: 
            // Logout api meghívása, ha van tokenünk
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { 
                SetProperty(ref _username, value);
                // Értesíti a LoginCommandot, hogy újra ellenőrizze le, hogy lefuttatható-e
                LoginCommand.NotifyCanExecuteChanged(); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { 
                SetProperty(ref _password, value); 
                LoginCommand.NotifyCanExecuteChanged(); }
        }
        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        public IAsyncRelayCommand LoginCommand { get; set; }

        private bool CanLogin()
        {
            // Akkor lesz IGAZ érték, ha nem üres a Felhasználónév és a Jelszó
            return !string.IsNullOrWhiteSpace(_username) && !string.IsNullOrWhiteSpace(_password);
        }

        private async Task Login()
        {
            string response = await _repository.AuthenticateAsync(_username, _password);
            if (response == "Sikeres bejelentkezés.")
            {
                Username = null;
                Password = null;
                ErrorMessage = null;
                // Tovább navigáljuk a felhasználót
                await Shell.Current.GoToAsync(nameof(MainPage));
            }
            else
            {
                // Megjelenítjük a hibaüzenetet
                ErrorMessage = response;
            }
        }
    }
}
