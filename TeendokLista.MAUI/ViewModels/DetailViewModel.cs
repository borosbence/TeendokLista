using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TeendokLista.MAUI.Models;
using TeendokLista.MAUI.Repositories;

namespace TeendokLista.MAUI.ViewModels
{
    [QueryProperty(nameof(Feladat), "Feladat")]
    public class DetailViewModel : ObservableObject
    {
        private IGenericRepository<Feladat> _repository;
        public DetailViewModel(IGenericRepository<Feladat> repository)
        {
            _repository = repository;
            SaveCommandAsync = new AsyncRelayCommand(Save);
            DeleteCommandAsync = new AsyncRelayCommand(Delete);
        }

        private Feladat _feladat;
        public Feladat Feladat
        {
            get { return _feladat; }
            set { SetProperty(ref _feladat, value); }
        }

        public IAsyncRelayCommand SaveCommandAsync { get; set; }
        public IAsyncRelayCommand DeleteCommandAsync { get; set; }

        private async Task Delete()
        {
            bool letezik = await _repository.ExistsAsync(_feladat.Id);
            if (letezik)
            {
                await _repository.DeleteAsync(_feladat.Id);
            }
            MessagingCenter.Send(this, "UpdateView", Feladat);
            await Shell.Current.GoToAsync("..");
        }

        private async Task Save()
        {
            bool letezik = await _repository.ExistsAsync(_feladat.Id);
            if (letezik)
            {
                await _repository.UpdateAsync(_feladat.Id, _feladat);
            }
            else
            {
                await _repository.InsertAsync(_feladat);
            }
            MessagingCenter.Send(this, "UpdateView", Feladat);
            await Shell.Current.GoToAsync("..");
        }
    }
}
