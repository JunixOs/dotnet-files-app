using Construccion_II___App_API_Rest.Src;
using Construccion_II___App_API_Rest.Src.Exceptions.Actor;

namespace Construccion_II___App_API_Rest.Src.Actors.Application
{
    public class GetActorById
    {
        private readonly IActorRepository _actorRepository;

        public GetActorById(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<Result<ActorModel>> Execute(string id)
        {
            ActorModel? actorModel = await _actorRepository.GetById(id);

            if(actorModel is null)
            {
                throw new NotFoundActorByIdException();
            }

            return Result<ActorModel>.Ok(actorModel);
        }
    }
}