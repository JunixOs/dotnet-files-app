using Mobile_App___dotNET_MAUI.ViewModels;

namespace Mobile_App___dotNET_MAUI;

public partial class MainPage : ContentPage
{
    private readonly FilmViewModel _filmViewModel;

    public MainPage()
    {
        InitializeComponent();
        _filmViewModel = new FilmViewModel();
        BindingContext = _filmViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_filmViewModel.CollectionFilms.Count == 0)
        {
            await _filmViewModel.LoadFilmsAsync();
        }
    }
}
