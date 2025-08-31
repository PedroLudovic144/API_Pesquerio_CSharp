using System;

namespace PesqueiroNetApi.Entities
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public DateOnly DtCompra { get; set; }
        public decimal ValorTotal { get; set; }

        public ICollection<CompraCliente>? CompraClientes { get; set; }
        public ICollection<Produto>? Produtos { get; set; }
    }
}
