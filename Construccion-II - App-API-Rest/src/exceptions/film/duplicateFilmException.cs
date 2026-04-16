namespace Construccion_II___App_API_Rest.Src.Exceptions.Film
{
    // Para crear una excepcion personalizada se hereda de "Exception"
    public class DuplicateFilmException : AppException
    {
        // base() se usa para acceder a miembros (métodos, propiedades o constructores) de su clase base o padre. 
        // Permite reutilizar código, llamar a constructores de la clase base al instanciar la clase hija y acceder a métodos ocultos
        public DuplicateFilmException() : base("La pelicula ya existe" , "DUPLICATE_FILM" , 400) { }
    }
}