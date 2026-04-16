using System.Runtime.Serialization;
using Construccion_II___App_API_Rest.Src.Films;

namespace Construccion_II___App_API_Rest.Src.Actors
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class ActorModel
    {
        [DataMember(Order = 1)]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [DataMember(Order = 2)]
        public string? FirstName { get; set; }
        
        [DataMember(Order = 3)]
        public string? LastName { get; set; }
        
        [DataMember(Order = 4)]
        public DateOnly Birthday { get; set; }
        
        [DataMember(Order = 5)]
        public List<FilmModel> FilmModels { get; set; } = new();
    }
}