using Microsoft.EntityFrameworkCore;

namespace Construccion_II___App_API_Rest.Src.Actors
{
    public class ActorRepository : IActorRepository
    {
        private readonly AppDbContext _appDbContext;

        public ActorRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<ActorModel>> GetAll()
        {
            List<ActorModel> actorModels = await _appDbContext.Actors.ToListAsync();

            return actorModels;
        }

        public Task<ActorModel?> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> Save(ActorModel actorModel)
        {
            throw new NotImplementedException();
        }
    }
}