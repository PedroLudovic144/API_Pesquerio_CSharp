namespace PesqueiroNetApi.Entities
{
    public class EspecieLago
    {
        public int IdEspecie { get; set; }
        public Especie? Especie { get; set; }

        public int IdLago { get; set; }
        public Lago? Lago { get; set; }

        public int QtdPeixes { get; set; }
    }
}
