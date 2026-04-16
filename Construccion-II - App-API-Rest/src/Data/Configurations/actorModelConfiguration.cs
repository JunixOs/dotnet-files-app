using Construccion_II___App_API_Rest.Src.Actors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Construccion_II___App_API_Rest.Src.Data.Configurations
{
    public class ActorModelConfiguration : IEntityTypeConfiguration<ActorModel>
    {
        public void Configure(EntityTypeBuilder<ActorModel> builder)
        {
            // Definir la PK
            builder.HasKey(a => a.Id);

            // Configuracion de Nombres de Actor
            builder.Property(a => a.FirstName)
                .HasMaxLength(255)
                .IsRequired();

            // Configuracion de Apellidos de Actor
            builder.Property(a => a.LastName)
                .HasMaxLength(255)
                .IsRequired();

            // Configuracion de Cumpleaños
            builder.Property(a => a.Birthday)
                .IsRequired();

            // Añadir datos de prueba
            
        }
    }
}