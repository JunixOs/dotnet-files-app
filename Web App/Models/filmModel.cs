using System.ComponentModel.DataAnnotations;

namespace Web_App.Models
{
    public class FilmModel
    {
        public string? Id { get; set; }
        
        [Display(Name = "Nombre de la Pelicula")]
        public string? Name { get; set; }

        [Display(Name = "Fecha de Estreno")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly PremierDate { get; set; }

        [Display(Name = "Actores")] // Esto sirve para mostrar un nombre mas amigable al hacer "asp-for" en un "label"
        public List<ActorModel> ActorModels { get; set; } = new();
    }
}
