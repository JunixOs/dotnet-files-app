using Background_Service.Models;
using System.Net.Http.Json;

namespace Background_Service
{
    public class Worker : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<Worker> _logger;

        public Worker(IHttpClientFactory httpClientFactory, ILogger<Worker> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Servicio iniciado");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var client = _httpClientFactory.CreateClient();

                    ResultModel<List<FilmModel>> response = await client.GetFromJsonAsync<ResultModel<List<FilmModel>>>(
                        "http://127.0.0.1:5007/api/v1/films/query/list-all",
                        stoppingToken);

                    _logger.LogInformation($"Respuesta de la API desde http://127.0.0.1:5007/api/v1/films/query/list-all");
                    _logger.LogInformation($"Fecha y Hora de Respuesta: {DateTime.Now.ToString()}");
                    _logger.LogInformation($"Exito: {response.Success}");
                    _logger.LogInformation("Datos Recibidos: ");

                    foreach (var item in response.Data)
                    {
                        _logger.LogInformation("---------");
                        _logger.LogInformation($"ID Pelicula: {item.Id}");
                        _logger.LogInformation($"Nombre Pelicula: {item.Name}");
                        _logger.LogInformation($"Fecha de Estreno de la Pelicula: {item.PremierDate.ToString()}");
                        _logger.LogInformation("---------");
                    }
                    _logger.LogInformation($"Mensajes extra: {response.Message}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener los resultados.");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            _logger.LogInformation("Servicio detenido");
        }
    }
}
