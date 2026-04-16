namespace Construccion_II___App_API_Rest.Src.Films.Application
{
    public class UpdateFilm
    {
        private readonly IFilmRepository _filmRepository;

        public UpdateFilm(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public async Task<Result<string>> Execute(FilmModel filmModelNew)
        {
            string result = await _filmRepository.Update(filmModelNew);
            return Result<string>.Ok(result);
        }
    }
}