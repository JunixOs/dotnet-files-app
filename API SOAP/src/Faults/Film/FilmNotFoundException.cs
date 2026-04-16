namespace API_SOAP.Src.Exceptions.Film
{
    internal class FilmNotFoundException : AppException
    {
        public FilmNotFoundException() : base("No se encontro la pelicula especificada" , 401 , "FILM_NOT_FOUND") { }
    }
}