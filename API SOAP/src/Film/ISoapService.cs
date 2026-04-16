using API_SOAP.Controllers.Film;
using CoreWCF;
using Microsoft.AspNetCore.Mvc;

namespace API_SOAP.Src.Film
{
    [ServiceContract]
    internal interface IFilmSoapService
    {
        [OperationContract]
        public string SaveFilm(FilmModel filmModel);
    }
}