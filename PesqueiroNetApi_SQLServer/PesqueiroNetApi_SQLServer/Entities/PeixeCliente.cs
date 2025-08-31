namespace PesqueiroNetApi.Entities
{
    public class PeixeCliente
    {
        public int IdCliente { get; set; }
        public Cliente? Cliente { get; set; }

        public int IdPeixeCapturado { get; set; }
        public PeixeCapturado? PeixeCapturado { get; set; }
    }
}
