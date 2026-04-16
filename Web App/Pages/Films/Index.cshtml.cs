using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_App.Models;

namespace Web_App.Pages.Films
{
    public class FilmIndexPageModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public List<FilmModel> FilmModels = new();

        public string? ErrorMessage;

        public FilmIndexPageModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task OnGet(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync("http://127.0.0.1:5007/api-rest/v1/film/list-all");

                var json = await response.Content.ReadAsStringAsync();

                ResponseModel<List<FilmModel>> responseContent = JsonSerializer.Deserialize<ResponseModel<List<FilmModel>>>(json , new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (responseContent.Success)
                {
                    FilmModels = responseContent.Data;
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