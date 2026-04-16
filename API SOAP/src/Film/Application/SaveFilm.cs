namespace API_SOAP.Src.Film.Application
{
    internal class SaveFilm
    {
        private readonly IFilmRepository _filmRepository;

        public SaveFilm(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }
    }
}