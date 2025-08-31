namespace PesqueiroNetApi.Entities
{
    public class Lago
    {
        public int IdLago { get; set; }
        public string? NomeLago { get; set; }

        public ICollection<EspecieLago>? EspecieLagos { get; set; }
    }
}
