using System.ComponentModel.DataAnnotations;

namespace Web_App.Models
{
    public class ActorModel
    {
        public string? Id { get; set; }
        
        [Display(Name = "Nombre Completo")]
        public string? FirstName { get; set; }

        [Display(Name = "Apellidos")]
        public string? LastName { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly BirthDay { get; set; }

        [Display(Name = "Peliculas")]
        public List<FilmModel> FilmModels { get; set; } = new();
    }
}