﻿using ApiClient.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TeendokLista.MAUI.Messages;
using TeendokLista.MAUI.Models;
using TeendokLista.MAUI.Services;
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
            SelectCommandAsync = new AsyncRelayCommand<FeladatModel>(ShowItem);
            LogoutCommandAsync = new AsyncRelayCommand(Logout);
            RegisterUpdate();
        }

        public string? DisplayName => CurrentUser.FelhasznaloNev;

        // korábban ObservableCollection<FeladatModel>
        private List<FeladatModel> _feladatok = [];
        public List<FeladatModel> Feladatok
        {
            get { return _feladatok; }
            set { SetProperty(ref _feladatok, value); }
        }

        public IAsyncRelayCommand<FeladatModel> SelectCommandAsync { get; set; }
        public IAsyncRelayCommand NewCommandAsync { get; set; }
        public IAsyncRelayCommand LogoutCommandAsync { get; set; }

        public async Task LoadData()
        {
            var result = await _repository.GetAllAsync();
            Feladatok = result != null ? new List<FeladatModel>(result) : [];
        }

        // Regisztrálás az üzenetközpont üzenetire
        // Ha jön üzenet a DetailViewModeltől, pl. egy Feladat objektum, akkor frissítse a meglévő listát
        private void RegisterUpdate()
        {
            WeakReferenceMessenger.Default.Register<MainPageMessage>(this, (r, m) =>
            {
                var message = m.Value;
                if (message.Action == ListAction.Add && message.Item.Id > 0)
                {
                    Feladatok.Add(message.Item);
                }
                else if (message.Action == ListAction.Delete)
                {
                    Feladatok.Remove(message.Item);
                }
            });
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
