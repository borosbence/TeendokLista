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
            LoginCommand = new AsyncRelayCommand(Login, CanLogin);
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); LoginCommand.NotifyCanExecuteChanged(); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); LoginCommand.NotifyCanExecuteChanged(); }
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
            return !string.IsNullOrWhiteSpace(_username) && !string.IsNullOrWhiteSpace(_password);
        }

        private async Task Login()
        {
            ErrorMessage = _repository.Authenticate(_username, _password);
            if (ErrorMessage == "Sikeres bejelentkezés.")
            {
                await Shell.Current.GoToAsync(nameof(MainPage));
            }
            Username = null;
            Password = null;
            ErrorMessage = null;
        }
    }
}
