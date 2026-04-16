using Construccion_II___App_API_Rest.Src.Films.Application;

namespace Construccion_II___App_API_Rest.Src.Films
{
    public class FilmSoapService : IFilmSoapService
    {
        private readonly SaveFilm _saveFilm;

        public FilmSoapService(SaveFilm saveFilm)
        {
            _saveFilm = saveFilm;
        }

        public async Task<string> SaveFilm(FilmModel filmModel)
        {
            string result = await _saveFilm.Execute(filmModel);

            return result;
        }
    }
}