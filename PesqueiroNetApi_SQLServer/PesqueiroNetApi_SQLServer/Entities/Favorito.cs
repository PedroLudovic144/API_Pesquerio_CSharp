namespace PesqueiroNetApi.Entities
{
    public class Favorito
    {
        public int IdPesqueiro { get; set; }
        public Pesqueiro? Pesqueiro { get; set; }

        public int IdCliente { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
