using CoreWCF;

namespace Construccion_II___App_API_Rest.Src.Actors
{
    [ServiceContract]
    public interface IActorSoapService
    {
        [OperationContract]
        public Task<string> SaveActor(ActorModel actorModel);
    }
}