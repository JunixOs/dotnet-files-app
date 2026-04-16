using Construccion_II___App_API_Rest.src.films.application;
using Construccion_II___App_API_Rest.Src.Films.Application;
using Construccion_II___App_API_Rest.Src;
using Microsoft.AspNetCore.Mvc;

namespace Construccion_II___App_API_Rest.Src.Films
{
    [ApiController]
    [Route("api-rest/v1/film")]
    public class FilmController : Controller
    {
        private readonly ListFilms _listFilms;
        private readonly FindFilmById _findFilmById;
        private readonly UpdateFilm _updateFilm;
        private readonly DeleteFilmById _deleteFilmById;

        public FilmController(ListFilms listFilms , FindFilmById findFilmById , UpdateFilm updateFilm , DeleteFilmById deleteFilmById)
        {
            _listFilms = listFilms;
            _findFilmById = findFilmById;
            _updateFilm  = updateFilm;
            _deleteFilmById = deleteFilmById;
        }

        [HttpGet(Name = "QueryHealthChecker")]
        public string Health()
        {
            return "Film Controller is Running!";
        }

        [HttpGet("list-all" , Name = "ListAllFilms")]
        public async Task<IActionResult> listAllFilms()
        {
            Result<List<FilmModel>> result = await _listFilms.Execute();
            return StatusCode(200 , result);
        }

        [HttpGet("find-by-id/{id}" , Name = "FindFilmById")]
        public async Task<IActionResult> FindFilmById(Guid id)
        {
            Result<FilmModel?> result = await _findFilmById.Execute(id);
            return StatusCode(200, result);
        }

        [HttpPut("update" , Name = "UpdateFilm")]
        public async Task<IActionResult> UpdateFilm(FilmModel filmModel)
        {
            Result<string> result = await _updateFilm.Execute(filmModel);

            return StatusCode(201 , result);
        }

        [HttpDelete("delete/{id}" , Name = "DeleteFilm")]
        public async Task<IActionResult> DeleteFilm(Guid id)
        {
            Result<string> result = await _deleteFilmById.Execute(id);

            return StatusCode(200 , result);
        }
    }
}