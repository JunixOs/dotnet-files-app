using Construccion_II___App_API_Rest.Src.Actors;
using Construccion_II___App_API_Rest.Src.Films;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    // Constructor del AppDbContext
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Metodo para registrar la tabla Films
    public DbSet<FilmModel> Films { get; set; }
    public DbSet<ActorModel> Actors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuracion de relacion n:m entre "Film" y "Actor"
        modelBuilder.Entity<FilmModel>()
            .HasMany(f => f.ActorModels)
            .WithMany(a => a.FilmModels)
            .UsingEntity(j => j.ToTable("FilmActor"));

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
