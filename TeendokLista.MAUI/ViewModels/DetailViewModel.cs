using ApiClient.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TeendokLista.MAUI.Models;
using TeendokLista.MAUI.Views;

namespace TeendokLista.MAUI.ViewModels
{
    [QueryProperty(nameof(Feladat), "Feladat")]
    public class DetailViewModel : ObservableObject
    {
        private IGenericRepository<FeladatModel> _repository;

        public DetailViewModel(IGenericRepository<FeladatModel> repository)
        {
            _repository = repository;
            SaveCommandAsync = new AsyncRelayCommand(Save);
            DeleteCommandAsync = new AsyncRelayCommand(Delete);
        }

        // Ennek a feladatnak a részleivel töltjük ki az űrlapot
        private FeladatModel _feladat = new();
        public FeladatModel Feladat
        {
            get { return _feladat; }
            set { SetProperty(ref _feladat, value); }
        }

        public IAsyncRelayCommand SaveCommandAsync { get; set; }
        public IAsyncRelayCommand DeleteCommandAsync { get; set; }


        private async Task Save()
        {
            bool letezik = await _repository.ExistsByIdAsync(_feladat.Id);
            if (letezik)
            {
                // Meglévő elem frissítése
                await _repository.UpdateAsync(_feladat.Id, _feladat);
            }
            else
            {
                // Új elem beillesztése
                await _repository.InsertAsync(_feladat);
            }
            // Üzenet küldése a fő ablaknak, ami feliratkozott az UpdateView csatornára
            MessagingCenter.Send(this, "UpdateView", Feladat);
            // Visszaugrik a szülő ablakra
            // await Shell.Current.GoToAsync("..");
            await Shell.Current.GoToAsync(nameof(MainPage));
        }

        private async Task Delete()
        {
            bool letezik = await _repository.ExistsByIdAsync(_feladat.Id);
            if (letezik)
            {
                await _repository.DeleteAsync(_feladat.Id);
            }
            MessagingCenter.Send(this, "UpdateView", Feladat);
            await Shell.Current.GoToAsync(nameof(MainPage));
        }
    }
}
