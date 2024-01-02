using TeendokLista.MAUI.ViewModels;

namespace TeendokLista.MAUI.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}