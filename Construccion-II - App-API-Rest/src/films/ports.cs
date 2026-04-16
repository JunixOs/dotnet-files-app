namespace Construccion_II___App_API_Rest.Src.Films
{
    public interface IFilmRepository
    {
        public Task<FilmModel?> GetById(Guid id);
        public Task<string> Save(FilmModel filmModel);
        public Task<string> Update(FilmModel filmModel);
        public Task<List<FilmModel>> GetAll();
        public Task<bool> ExistsById(Guid id);
        public Task<string> DeleteById(Guid id);
    }
}