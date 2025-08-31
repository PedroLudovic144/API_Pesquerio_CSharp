namespace PesqueiroNetApi.Entities
{
    public class CompraCliente
    {
        public int IdCliente { get; set; }
        public Cliente? Cliente { get; set; }

        public int IdCompra { get; set; }
        public Compra? Compra { get; set; }
    }
}
