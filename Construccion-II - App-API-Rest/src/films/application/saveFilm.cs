namespace Construccion_II___App_API_Rest.Src.Films.Application
{
    public class SaveFilm
    {
        private readonly IFilmRepository _filmRepository;

        public SaveFilm(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public async Task<string> Execute(FilmModel filmModel)
        {
            bool filmExists = await _filmRepository.ExistsById(filmModel.Id);

            if (filmExists)
            {
                return "La pelicula ya existe";
            }

            string result = await _filmRepository.Save(filmModel);
            
            return result;
        }
    }
}