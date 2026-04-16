namespace Construccion_II___App_API_Rest.Src.Exceptions.Actor
{
    public class NotFoundActorByIdException : AppException
    {
        public NotFoundActorByIdException() : base("No se encontro ningun actor" , "ACTOR_NOT_FOUND" , 404) { }
    }
}