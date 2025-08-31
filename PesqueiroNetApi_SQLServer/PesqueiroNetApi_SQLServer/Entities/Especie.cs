namespace PesqueiroNetApi.Entities
{
    public class Especie
    {
        public int IdEspecie { get; set; }
        public string? NomeEspecie { get; set; }
        public decimal ValorEspecie { get; set; }
        public string? FornecedorEspecie { get; set; }

        public ICollection<PeixeCapturado>? PeixesCapturados { get; set; }
        public ICollection<EspecieLago>? EspecieLagos { get; set; }
    }
}
