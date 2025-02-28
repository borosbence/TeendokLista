using TeendokLista.MAUI.ViewModels;

namespace TeendokLista.MAUI.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel;

    public MainPage(MainViewModel viewModel)
    {
        _viewModel = viewModel;
        BindingContext = _viewModel;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        await _viewModel.LoadDataAsync();
    }
}