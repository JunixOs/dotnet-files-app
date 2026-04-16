using Construccion_II___App_API_Rest.Src;

namespace Construccion_II___App_API_Rest.Src.Films.Application
{
    public class ListFilms
    {
        private readonly IFilmRepository _filmRepository;

        public ListFilms(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public async Task<Result<List<FilmModel>>> Execute()
        {
            List<FilmModel> listFilmModel = await _filmRepository.GetAll();
            return Result<List<FilmModel>>.Ok(listFilmModel);
        }
    }
}