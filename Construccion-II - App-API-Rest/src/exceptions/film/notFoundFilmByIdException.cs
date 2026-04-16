namespace Construccion_II___App_API_Rest.Src.Exceptions.Film
{
    public class NotFoundFilmByIdException : AppException
    {
        public NotFoundFilmByIdException() : base("No se encontró ninguna pelicula" , "FILM_NOT_FOUND" , 404) { }
    }
}