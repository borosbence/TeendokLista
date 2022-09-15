﻿using CommunityToolkit.Mvvm.ComponentModel;
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
            Task.Run(async () => await LoadData()).Wait();
            NewCommandAsync = new AsyncRelayCommand(AddItem);
            SelectCommandAsync = new AsyncRelayCommand<Feladat>(f => ShowDetail(f));
            UpdateView();
        }

        private ObservableCollection<Feladat> _feladatok = new ObservableCollection<Feladat>();
        public ObservableCollection<Feladat> Feladatok
        {
            get { return _feladatok; }
            set { SetProperty(ref _feladatok, value); }
        }

        public IAsyncRelayCommand<Feladat> SelectCommandAsync { get; set; }
        public IAsyncRelayCommand NewCommandAsync { get; set; }

        private async Task LoadData()
        {
            var result = await _repository.GetAllAsync();
            Feladatok = new ObservableCollection<Feladat>(result);
        }

        private void UpdateView()
        {
            MessagingCenter.Subscribe<DetailViewModel, Feladat>(this, "UpdateView", async (sender, feladat) =>
            {
                //Feladatok.Clear();
                //var feladatok = _repository.GetAllAsync().Result;
                //foreach (var f in feladatok)
                //{
                //    Feladatok.Add(f);
                //}
                await LoadData();
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
