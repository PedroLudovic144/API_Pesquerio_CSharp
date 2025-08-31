namespace PesqueiroNetApi.Entities
{
    public class Comentario
    {
        public int IdComentario { get; set; }
        public string? Texto { get; set; }
        public int Avaliacao { get; set; }

        public int IdPesqueiro { get; set; }
        public Pesqueiro? Pesqueiro { get; set; }

        public ICollection<ClienteComentario>? ClienteComentarios { get; set; }
    }
}
