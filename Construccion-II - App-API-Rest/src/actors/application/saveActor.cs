namespace Construccion_II___App_API_Rest.Src.Actors.Application
{
    public class SaveActor
    {
        private readonly IActorRepository _actorRepository;

        public SaveActor(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<string> Execute(ActorModel actorModel)
        {
            string result = await _actorRepository.Save(actorModel);

            return result;
        }
    }
}