using ApiClient.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TeendokLista.MAUI.Models;
using TeendokLista.MAUI.Views;

namespace TeendokLista.MAUI.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IGenericRepository<FeladatModel> _repository;

        public MainViewModel(IGenericRepository<FeladatModel> repository)
        {
            _repository = repository;
            NewCommandAsync = new AsyncRelayCommand(AddItem);
            SelectCommandAsync = new AsyncRelayCommand<FeladatModel>(ShowItem!);
            LogoutCommandAsync = new AsyncRelayCommand(Logout);
            Task.Run(LoadData);
        }

        private ObservableCollection<FeladatModel> _feladatok = [];
        public ObservableCollection<FeladatModel> Feladatok
        {
            get { return _feladatok; }
            // aszinkron feltöltés miatt kell
            set { SetProperty(ref _feladatok, value); }
        }

        public IAsyncRelayCommand<FeladatModel> SelectCommandAsync { get; set; }
        public IAsyncRelayCommand NewCommandAsync { get; set; }
        public IAsyncRelayCommand LogoutCommandAsync { get; set; }

        private async Task LoadData()
        {
            var result = await _repository.GetAllAsync();
            Feladatok = result != null ? new ObservableCollection<FeladatModel>(result) : [];
        }

        private async Task ShowItem(FeladatModel feladat)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Feladat", feladat }
            };
            // Navigáció a másik Page-re
            await Shell.Current.GoToAsync(nameof(DetailPage), navigationParameter);
        }

        private async Task AddItem()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Feladat", new FeladatModel() }
            };
            await Shell.Current.GoToAsync(nameof(DetailPage), navigationParameter);
        }

        private async Task Logout()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
