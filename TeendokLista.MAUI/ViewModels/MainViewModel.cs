using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TeendokLista.MAUI.Models;
using TeendokLista.MAUI.Repositories;
using TeendokLista.MAUI.Views;

namespace TeendokLista.MAUI.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private IGenericRepository<Feladat> _repository;
        public MainViewModel(IGenericRepository<Feladat> repository)
        {
            _repository = repository;
            // TODO: db repository esetén csere
            Feladatok = new ObservableCollection<Feladat>(_repository.GetAllAsync().Result);
            NewCommandAsync = new AsyncRelayCommand(AddItem);
            SelectCommandAsync = new AsyncRelayCommand<Feladat>(f => ShowDetail(f));
            UpdateView();
        }

        public ObservableCollection<Feladat> Feladatok { get; set; }
        public IAsyncRelayCommand<Feladat> SelectCommandAsync { get; set; }
        public IAsyncRelayCommand NewCommandAsync { get; set; }

        private void UpdateView()
        {
            MessagingCenter.Subscribe<DetailViewModel, Feladat>(this, "UpdateView", (sender, feladat) =>
            {
                Feladatok.Clear();
                var feladatok = _repository.GetAllAsync().Result;
                foreach (var f in feladatok)
                {
                    Feladatok.Add(f);
                }
            });
        }

        private async Task ShowDetail(Feladat feladat)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Feladat", feladat }
            };
            await Shell.Current.GoToAsync(nameof(DetailPage), navigationParameter);
        }

        private async Task AddItem()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Feladat", new Feladat() }
            };
            await Shell.Current.GoToAsync(nameof(DetailPage), navigationParameter);
        }
    }
}
