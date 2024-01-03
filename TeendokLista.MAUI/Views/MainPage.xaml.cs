using TeendokLista.MAUI.ViewModels;

namespace TeendokLista.MAUI.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _vm;
    public MainPage(MainViewModel mainViewModel)
    {
        _vm = mainViewModel;
        BindingContext = _vm;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        await _vm.LoadData();
    }
}