using Construccion_II___App_API_Rest.Src.Actors;
using Construccion_II___App_API_Rest.Src.Exceptions.Film;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace Construccion_II___App_API_Rest.Src.Films
{
    public class FilmRepository : IFilmRepository
    {
        private readonly AppDbContext _app_db_context;

        public FilmRepository(AppDbContext appDbContext)
        {
            _app_db_context = appDbContext;
        }

        public async Task<string> Update(FilmModel filmModelNew) { 
            var filmOld = await _app_db_context.Films.Include(f => f.ActorModels).FirstOrDefaultAsync(f => f.Id == filmModelNew.Id); 
            
            if (filmOld == null) 
            {
                throw new NotFoundFilmByIdException();
            }; 
            
            _app_db_context.Entry(filmOld).CurrentValues.SetValues(filmModelNew); 
            
            filmOld.ActorModels.Clear(); 
            
            if (filmModelNew.ActorModels != null) 
            { 
                var actors = await _app_db_context.Actors.Where(a => filmModelNew.ActorModels.Select(x => x.Id).Contains(a.Id)).ToListAsync(); 
                
                foreach (var actor in actors) 
                { 
                    filmOld.ActorModels.Add(actor); 
                } 
            } 
            
            await _app_db_context.SaveChangesAsync(); 
            
            return "Los datos se actualizaron con exito"; 
        }

        public async Task<List<FilmModel>> GetAll()
        {
            // Asi se obtienen todos los elementos como una lista
            List<FilmModel> listFilmModel = await _app_db_context.Films
                                                                .Include(f => f.ActorModels) // Siempre usa esto para traer datos de otra tabla relacionada
                                                                .ToListAsync();
            return listFilmModel;
        }

        public async Task<FilmModel?> GetById(Guid id)
        {
            FilmModel? filmModel = await _app_db_context.Films
                                                        .Include(f => f.ActorModels)
                                                        .AsNoTracking()
                                                        .FirstOrDefaultAsync(f => f.Id == id); // Esto para traer solo los que cumplen una condicion
            if (filmModel is null) // Si quieres verificar que sea null usa "is"
            {
                throw new NotFoundFilmByIdException();
            }
            return filmModel;
        }

        public async Task<string> Save(FilmModel filmModel)
        {
            // evitar null
            filmModel.ActorModels ??= new List<ActorModel>();

            var newActorIds = filmModel.ActorModels
                .Select(x => x.Id)
                .ToList();

            var actors = await _app_db_context.Actors
                .Where(a => newActorIds.Contains(a.Id))
                .ToListAsync();

            var film = new FilmModel
            {
                Id = filmModel.Id,
                Name = filmModel.Name,
                PremierDate = filmModel.PremierDate,
                ActorModels = actors
            };

            _app_db_context.Films.Add(film);

            await _app_db_context.SaveChangesAsync();

            return "Pelicula guardada con éxito";
        }

        public async Task<bool> ExistsById(Guid id)
        {
            return await _app_db_context.Films
                                .AnyAsync(f => f.Id == id);
        }

        public async Task<string> DeleteById(Guid id)
        {
            FilmModel? filmModel = await _app_db_context.Films
                                            .Include(f => f.ActorModels)
                                            .FirstOrDefaultAsync(f => f.Id == id);

            if (filmModel is null)
            {
                throw new NotFoundFilmByIdException();
            }

            _app_db_context.Films.Remove(filmModel);
            await _app_db_context.SaveChangesAsync();

            return "Pelicula eliminada con exito";
        }
    }
}