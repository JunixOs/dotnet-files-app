using Construccion_II___App_API_Rest.Src;

namespace Construccion_II___App_API_Rest.Src.Actors.Application
{
    public class ListAllActors
    {
        private readonly IActorRepository _actorRepository;

        public ListAllActors(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<Result<List<ActorModel>>> Execute()
        {
            List<ActorModel> actorModels = await _actorRepository.GetAll();

            return Result<List<ActorModel>>.Ok(actorModels);
        }
    }
}