using Construccion_II___App_API_Rest.Src.Films;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Construccion_II___App_API_Rest.Src.Data.Configurations
{
    public class FilmModelConfiguratiion : IEntityTypeConfiguration<FilmModel>
    {
        public void Configure(EntityTypeBuilder<FilmModel> builder)
        {
            // Definir PK
            builder.HasKey(f => f.Id);

            // Configuracion de Nombre
            builder.Property(f => f.Name)
                .HasMaxLength(255)
                .IsRequired();

            // Configuracion de Fecha de Estreno
            builder.Property(a => a.PremierDate)
                .IsRequired();
        }
    }
}