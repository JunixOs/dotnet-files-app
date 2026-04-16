namespace Construccion_II___App_API_Rest.Src.Films.Application
{
    public class DeleteFilmById
    {
        private readonly IFilmRepository _filmRepository;

        public DeleteFilmById(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public async Task<Result<string>> Execute(Guid id)
        {
            string result = await _filmRepository.DeleteById(id);

            return Result<string>.Ok(result);
        }
    }
}