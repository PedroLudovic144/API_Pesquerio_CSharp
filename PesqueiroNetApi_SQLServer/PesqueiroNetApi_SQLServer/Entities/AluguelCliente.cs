namespace PesqueiroNetApi.Entities
{
    public class AluguelCliente
    {
        public int IdCliente { get; set; }
        public Cliente? Cliente { get; set; }

        public int IdAluguel { get; set; }
        public Aluguel? Aluguel { get; set; }
    }
}
