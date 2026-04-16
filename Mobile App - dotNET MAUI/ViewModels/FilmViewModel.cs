using Mobile_App___dotNET_MAUI.Models;
using Mobile_App___dotNET_MAUI.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mobile_App___dotNET_MAUI.ViewModels
{
    internal class FilmViewModel : INotifyPropertyChanged
    {
        private readonly FilmApiService _filmApiService;

        public ObservableCollection<FilmModel> CollectionFilms { get; set; } = new();

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public FilmViewModel()
        {
            _filmApiService = new FilmApiService();
        }

        public async Task LoadFilmsAsync()
        {
            try
            {
                IsLoading = true;
                CollectionFilms.Clear();

                ResponseModel<List<FilmModel>> response = await _filmApiService.GetAllFilms();

                foreach (var item in response.Data)
                {
                    CollectionFilms.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlertAsync("Error", ex.Message, "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}