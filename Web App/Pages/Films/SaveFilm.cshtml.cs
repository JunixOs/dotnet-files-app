using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_App.Models;

namespace Web_App.Pages.Films
{
    public class SaveFilmPageModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public FilmModel FilmModel { get; set; }

        public List<ActorModel> ActorModels { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public SaveFilmPageModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task OnGet(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"http://127.0.0.1:5007/api-rest/v1/actor/list-all/{id}");

                var json = await response.Content.ReadAsStringAsync();

                ResponseModel<List<ActorModel>> responseContent = JsonSerializer.Deserialize<ResponseModel<List<ActorModel>>>(json , new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

                if (responseContent.Success)
                {
                    ActorModels = responseContent.Data;
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

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {

                var actorsXml = "";

                if (FilmModel.ActorModels != null && FilmModel.ActorModels.Any())
                {
                    actorsXml += "<tem:ActorModels>";

                    foreach (var actor in FilmModel.ActorModels)
                    {
                        actorsXml += $@"
                            <tem:ActorModel>
                                <tem:Id>{actor.Id}</tem:Id>
                            </tem:ActorModel>";
                    }

                    actorsXml += "</tem:ActorModels>";
                }

                // Se usa "@" para poder crear Strings de multiples lineas
                var xml = @$"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'
                                xmlns:tem='http://tempuri.org/'>
                <soapenv:Header/>
                <soapenv:Body>
                    <tem:SaveFilm>
                        <tem:filmModel>
                            <tem:Name>{FilmModel.Name}</tem:Name>
                            <tem:PremierDate>{FilmModel.PremierDate:yyyy-MM-dd}</tem:PremierDate>
                            {actorsXml}
                        </tem:filmModel>
                    </tem:SaveFilm>
                </soapenv:Body>
                </soapenv:Envelope>";

                Console.WriteLine(xml);

                var content = new StringContent(xml, Encoding.UTF8, "text/xml");

                content.Headers.Add("SOAPAction", "http://tempuri.org/IFilmSoapService/SaveFilm");

                var response = await _httpClient.PostAsync("http://localhost:5007/api-soap/v1/film", content);

                var xmlResponse = await response.Content.ReadAsStringAsync();

                var doc = XDocument.Parse(xmlResponse);

                var fault = doc.Descendants()
                    .FirstOrDefault(x => x.Name.LocalName == "Fault");

                if (fault != null)
                {
                    var error = fault.Descendants()
                        .FirstOrDefault(x => x.Name.LocalName == "faultstring")
                        ?.Value;

                    throw new Exception(error ?? "Error desconocido en SOAP");
                }

                var result = doc
                    .Descendants()
                    .FirstOrDefault(x => x.Name.LocalName == "SaveFilmResult")
                    ?.Value;

                TempData["Success"] = result; // Esto sirve para enviar datos a otra pagina con redireccion
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