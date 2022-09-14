namespace TeendokLista.API.DTOs
{
    public class FeladatDto
    {
        public int Id { get; set; }
        public string Cim { get; set; } = null!;
        public string? Tartalom { get; set; }
        public DateTime Hatarido { get; set; }
        public bool Teljesitve { get; set; }
        public int FelhasznaloId { get; set; }
    }
}
