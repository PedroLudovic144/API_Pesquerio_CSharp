namespace PesqueiroNetApi.Entities
{
    public class Pesqueiro
    {
        public int IdPesqueiro { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Fotos { get; set; }

        public ICollection<Comentario>? Comentarios { get; set; }
        public ICollection<Favorito>? Favoritos { get; set; }
    }
}
