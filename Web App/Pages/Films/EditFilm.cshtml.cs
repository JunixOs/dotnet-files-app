using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_App.Models;

namespace Web_App.Pages.Films
{
    public class EditFilmPageMode : PageModel
    {
        private readonly HttpClient _httpClient;

        // "BindProperty" sirve para vincular automáticamente los datos de una 
        // solicitud HTTP (como un formulario POST) directamente a las propiedades públicas de esta clase
        [BindProperty]
        public FilmModel FilmModel { get; set; }

        public List<ActorModel> ActorModels { get; set; } = new();
        
        public string? ErrorMessage { get; set; }

        public EditFilmPageMode(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task OnGet(string id)
        {
            try
            {
                var responseFilm = await _httpClient.GetAsync($"http://127.0.0.1:5007/api-rest/v1/film/find-by-id/{id}");
                var responseActors = await _httpClient.GetAsync($"http://127.0.0.1:5007/api-rest/v1/actor/list-all");

                var jsonFilm = await responseFilm.Content.ReadAsStringAsync();
                var jsonActor = await responseActors.Content.ReadAsStringAsync();

                ResponseModel<FilmModel> responseFilmContent = JsonSerializer.Deserialize<ResponseModel<FilmModel>>(jsonFilm , new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
                ResponseModel<List<ActorModel>> responseActorContent = JsonSerializer.Deserialize<ResponseModel<List<ActorModel>>>(jsonActor , new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

                if (responseFilmContent.Success || responseActorContent.Success)
                {
                    FilmModel = responseFilmContent.Data;
                    ActorModels = responseActorContent.Data;
                }
                else
                {
                    ErrorMessage = $"Error: ";
                    ErrorMessage += $"Film: {responseFilmContent.Message ?? "Sin errores"}";
                    ErrorMessage += $"Actors: {responseActorContent.Message ?? "Sin errores"}";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var filmtoSave = new FilmModel
                {
                    Id = FilmModel.Id,
                    Name = FilmModel.Name,
                    PremierDate = FilmModel.PremierDate,
                    ActorModels = FilmModel.ActorModels
                };
                var json = JsonSerializer.Serialize(filmtoSave);

                var content = new StringContent(json , Encoding.UTF8 , "application/json");

                var response = await _httpClient.PutAsync("http://127.0.0.1:5007/api-rest/v1/film/update" , content);

                var result = await response.Content.ReadAsStringAsync();

                ResponseModel<string> resultContent = JsonSerializer.Deserialize<ResponseModel<string>>(result , new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

                TempData["Success"] = resultContent.Data;

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
                return Page();
            }
        }
    }
}