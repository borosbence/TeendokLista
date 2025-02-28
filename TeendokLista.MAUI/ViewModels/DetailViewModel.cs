using ApiClient.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TeendokLista.MAUI.Models;
using TeendokLista.MAUI.Views;

namespace TeendokLista.MAUI.ViewModels
{
    [QueryProperty(nameof(Feladat), "Reszletek")]
    public partial class DetailViewModel : ObservableObject
    {
        private readonly IGenericRepository<FeladatModel> _repository;

        public DetailViewModel(IGenericRepository<FeladatModel> repository)
        {
            _repository = repository;
        }

        // Ennek a feladatnak a részleteivel töltjük ki az űrlapot
        [ObservableProperty]
        private FeladatModel _feladat = new();

        [RelayCommand]
        private async Task SaveItem()
        {
            bool letezik = await _repository.ExistsByIdAsync(Feladat.Id);
            if (letezik)
            {
                // Meglévő elem frissítése
                await _repository.UpdateAsync(Feladat.Id, Feladat);
            }
            else
            {
                // Új elem beillesztése
                 await _repository.InsertAsync(Feladat);
            }
            // Visszaugrik a szülő ablakra
            await Shell.Current.GoToAsync(nameof(MainPage));
        }

        [RelayCommand]
        private async Task DeleteItem()
        {
            bool letezik = await _repository.ExistsByIdAsync(Feladat.Id);
            if (letezik)
            {
                await _repository.DeleteAsync(Feladat.Id);
            }
            await Shell.Current.GoToAsync(nameof(MainPage));
        }
    }
}
