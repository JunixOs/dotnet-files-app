using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Web_App.Models;

//  .cshtml. Maneja la lógica del lado del servidor, procesa solicitudes HTTP (GET/POST),
//  gestiona la interacción con bases de datos y prepara los datos para ser mostrados en la interfaz. 
namespace Web_App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public List<FilmModel> ListFilmModel { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // El metodo OnGet es un metodo especial que hace una peticion GET cuando se abre la pagina .cshtml asociada
        public async Task OnGet()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://127.0.0.1:5007/api-rest/v1/film/list-all");

                var json = await response.Content.ReadAsStringAsync();

                ResponseModel<List<FilmModel>> responseContent = JsonSerializer.Deserialize<ResponseModel<List<FilmModel>>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (responseContent.Success)
                {
                    ListFilmModel = responseContent.Data;
                }
                else
                {
                    ErrorMessage = $"Error API: {responseContent.Message}";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
            }
        }
    }
}
