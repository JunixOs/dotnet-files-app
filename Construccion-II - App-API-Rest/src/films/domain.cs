using System.Runtime.Serialization;
using Construccion_II___App_API_Rest.Src.Actors;

namespace Construccion_II___App_API_Rest.Src.Films
{
    [DataContract(Namespace = "http://tempuri.org/")]
    // Aqui va el modelo, no involucra Framework, ni consultas, ni nada de eso, solo C# puro
    public class FilmModel
    {
        [DataMember(Order = 1)]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [DataMember(Order = 2)]
        public string? Name { get; set; }
        
        [DataMember(Order = 3)]
        public DateOnly PremierDate { get; set; }
        
        [DataMember(Order = 4)]
        public List<ActorModel> ActorModels { get; set; } = new();
    }
}
