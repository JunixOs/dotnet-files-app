using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_App.Models;

namespace Web_App.Pages.Films
{
    public class ViewFilmPageModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public FilmModel FilmModel { get; set; }
        
        public string? ErrorMessage { get; set; }

        public ViewFilmPageModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task OnGet(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"http://127.0.0.1:5007/api-rest/v1/film/find-by-id/{id}");

                var json = await response.Content.ReadAsStringAsync();

                ResponseModel<FilmModel?> responseContent = JsonSerializer.Deserialize<ResponseModel<FilmModel?>>(json , new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

                if (responseContent.Success)
                {
                    if(responseContent.Data is null)
                    {
                        ErrorMessage = "Error: No se encontro la pelicula";
                    }
                    else
                    {
                        FilmModel = responseContent.Data;
                    }
                }
                else
                {
                    ErrorMessage = $"Error: {responseContent.Message}";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
            }
        }
    }
}