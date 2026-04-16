using Mobile_App___dotNET_MAUI.Models;
using System.Net.Http.Json;

namespace Mobile_App___dotNET_MAUI.Services
{
    internal class FilmApiService
    {
        private readonly HttpClient _httpClient;

        public FilmApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ResponseModel<List<FilmModel>>> GetAllFilms()
        {
            string endpointUrl = "http://localhost:5007/api/v1/films/query/list-all";

            ResponseModel<List<FilmModel>> response = await _httpClient.GetFromJsonAsync<ResponseModel<List<FilmModel>>>(endpointUrl) ?? new ResponseModel<List<FilmModel>>
            {
                Success = false,
                Message = "Occurrio un error al solicitar la informacion"
            };

            return response;
        }
    }
}