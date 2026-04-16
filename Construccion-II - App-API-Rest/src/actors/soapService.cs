using Construccion_II___App_API_Rest.Src.Actors.Application;

namespace Construccion_II___App_API_Rest.Src.Actors
{
    public class ActorSoapService : IActorSoapService
    {
        private readonly SaveActor _saveActor;

        public ActorSoapService(SaveActor saveActor)
        {
            _saveActor = saveActor;
        }

        public async Task<string> SaveActor(ActorModel actorModel)
        {
            string result = await _saveActor.Execute(actorModel);

            return result;
        }
    }
}