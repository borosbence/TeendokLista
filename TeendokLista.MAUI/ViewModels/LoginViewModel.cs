using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TeendokLista.MAUI.Repositories;
using TeendokLista.MAUI.Views;

namespace TeendokLista.MAUI.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IFelhasznaloRepository _repository;

        public LoginViewModel(IFelhasznaloRepository repository)
        {
            _repository = repository;
            // TODO: Logout api meghívása, ha van tokenünk
        }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string? _username;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string? _password;

        [ObservableProperty]
        private string? _errorMessage;

        private bool CanLogin()
        {
            // Akkor lesz IGAZ érték, ha nem üres a Felhasználónév és a Jelszó
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task Login()
        {
            string response = await _repository.AuthenticateAsync(Username, Password);
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
