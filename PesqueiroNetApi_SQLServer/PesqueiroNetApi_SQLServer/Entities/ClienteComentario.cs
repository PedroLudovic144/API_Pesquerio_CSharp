namespace PesqueiroNetApi.Entities
{
    public class ClienteComentario
    {
        public int IdComentario { get; set; }
        public Comentario? Comentario { get; set; }

        public int IdCliente { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
