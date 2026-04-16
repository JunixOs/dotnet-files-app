namespace API_SOAP.Src.Film
{
    internal class FilmModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public DateOnly PremierDate { get; set; }
    }
}