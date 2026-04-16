using Construccion_II___App_API_Rest.Src.Exceptions.Film;
using Construccion_II___App_API_Rest.Src.Films;
using Construccion_II___App_API_Rest.Src;

namespace Construccion_II___App_API_Rest.src.films.application
{
    public class FindFilmById
    {
        private readonly IFilmRepository _filmRepository;

        public FindFilmById(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public async Task<Result<FilmModel?>> Execute(Guid id)
        {
            FilmModel? FilmModel = await _filmRepository.GetById(id);
            if(FilmModel is null)
            {
                throw new NotFoundFilmByIdException();
            }
            return Result<FilmModel?>.Ok(FilmModel);
        }
    }
}
