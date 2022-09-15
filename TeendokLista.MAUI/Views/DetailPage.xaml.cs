using TeendokLista.MAUI.ViewModels;

namespace TeendokLista.MAUI.Views;

public partial class DetailPage : ContentPage
{
    public DetailPage(DetailViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}