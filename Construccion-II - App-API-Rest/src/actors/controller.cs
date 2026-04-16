using Construccion_II___App_API_Rest.Src.Actors.Application;
using Construccion_II___App_API_Rest.Src;
using Microsoft.AspNetCore.Mvc;

namespace Construccion_II___App_API_Rest.Src.Actors
{
    [ApiController]
    [Route("/api-rest/v1/actor")]
    public class ActorController : Controller
    {
        private readonly ListAllActors _listAllActors;
        private readonly GetActorById _getActorById;

        public ActorController(ListAllActors listAllActors , GetActorById getActorById)
        {
            _listAllActors = listAllActors;
            _getActorById = getActorById;
        } 

        [HttpGet("ActorHealth")]
        public Result<string> Health()
        {
            return Result<string>.Ok(
                "API REST de Actor esta corriendo!"
            );
        }

        [HttpGet("list-all" , Name = "ListAllActors")]
        public async Task<IActionResult> ListAllActors()
        {
            Result<List<ActorModel>> result = await _listAllActors.Execute();

            return StatusCode(200 , result);
        }

        [HttpGet("find-by-id/{id}" , Name = "FindActorById")]
        public async Task<IActionResult> FindActorById(string id)
        {
            Result<ActorModel> result = await _getActorById.Execute(id);

            return StatusCode(200 , result); 
        }
    }
}