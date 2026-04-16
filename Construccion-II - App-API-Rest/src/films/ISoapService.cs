using CoreWCF;

namespace Construccion_II___App_API_Rest.Src.Films
{
    [ServiceContract]
    public interface IFilmSoapService
    {
        [OperationContract]
        public Task<string> SaveFilm(FilmModel filmModel);
    }
}