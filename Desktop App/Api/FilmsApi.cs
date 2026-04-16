using System.Reflection;
using System.Text.Json;
using Desktop_App.Models;

namespace Desktop_App.Api
{
    internal class FilmsApi
    {
        public async Task<ResultModel<List<FilmModel>>> GetAllFilms()
        {
            using HttpClient client = new HttpClient();

            string endpointUrl = "http://127.0.0.1:5007/api/v1/films/query/list-all";

            ResultModel<List<FilmModel>> result = new ResultModel<List<FilmModel>>();
            
            try
            {
                var response = await client.GetStringAsync(endpointUrl);

                var json = JsonDocument.Parse(response);
                List<FilmModel> filmList = new List<FilmModel>();

                result.Success = json.RootElement.GetProperty("success").GetBoolean();
                if (!result.Success)
                {
                    return result;
                }
                result.Message = json.RootElement.GetProperty("message").GetString();

                foreach(var item in json.RootElement.GetProperty("data").EnumerateArray())
                {
                    filmList.Add(new FilmModel
                    {
                        Id = item.GetProperty("id").GetString() ?? "",
                        Name = item.GetProperty("name").GetString() ?? "",
                        PremierDate = DateOnly.Parse(item.GetProperty("premierDate").GetString()!)
                    });
                }

                result.Data = filmList;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }
    }
}