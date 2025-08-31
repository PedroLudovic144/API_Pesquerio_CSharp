namespace PesqueiroNetApi.Entities
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string? NomeProduto { get; set; }
        public decimal ValorProduto { get; set; }
        public int QtdProduto { get; set; }
        public string? Fornecedor { get; set; }

        public int? IdCompra { get; set; }
        public Compra? Compra { get; set; }
    }
}
