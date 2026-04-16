namespace Construccion_II___App_API_Rest.Src.Actors
{
    public interface IActorRepository
    {
        public Task<string> Save(ActorModel actorModel);
        public Task<List<ActorModel>> GetAll();
        public Task<ActorModel?> GetById(string id);
    }
}