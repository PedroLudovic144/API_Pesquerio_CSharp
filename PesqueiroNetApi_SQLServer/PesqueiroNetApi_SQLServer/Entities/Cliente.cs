namespace PesqueiroNetApi.Entities
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string? NomeCliente { get; set; }
        public string? SenhaCliente { get; set; }
        public string? EmailCliente { get; set; }

        public ICollection<AluguelCliente>? AluguelClientes { get; set; }
        public ICollection<CompraCliente>? CompraClientes { get; set; }
        public ICollection<PeixeCliente>? PeixeClientes { get; set; }
        public ICollection<ClienteComentario>? ClienteComentarios { get; set; }
        public ICollection<Favorito>? Favoritos { get; set; }
    }
}
