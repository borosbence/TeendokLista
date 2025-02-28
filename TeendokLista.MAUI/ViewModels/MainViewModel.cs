using ApiClient.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TeendokLista.MAUI.Models;
using TeendokLista.MAUI.Views;

namespace TeendokLista.MAUI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IGenericRepository<FeladatModel> _repository;

        public MainViewModel(IGenericRepository<FeladatModel> repository)
        {
            _repository = repository;
        }

        [ObservableProperty]
        private ObservableCollection<FeladatModel> _feladatok = [];

        public async Task LoadDataAsync()
        {
            var result = await _repository.GetAllAsync();
            Feladatok = result != null ? new ObservableCollection<FeladatModel>(result) : [];
        }

        [RelayCommand]
        private async Task SelecItem(FeladatModel feladat)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Reszletek", feladat }
            };
            // Navigáció a másik Page-re
            await Shell.Current.GoToAsync(nameof(DetailPage), navigationParameter);
        }

        [RelayCommand]
        private async Task NewItem()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Reszletek", new FeladatModel() }
            };
            await Shell.Current.GoToAsync(nameof(DetailPage), navigationParameter);
        }

        [RelayCommand]
        private async Task Logout()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
