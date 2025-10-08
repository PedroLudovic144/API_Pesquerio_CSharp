using System.Text.Json.Serialization;

namespace PesqueiroNetApi.Entities
{
    public class Lago
    {
        public int IdLago { get; set; }
        public string? NomeLago { get; set; }

        public int TamanhoLago { get; set; } // em metros quadrados

        public int IdPesqueiro { get; set; }

        [JsonIgnore]
        public Pesqueiro? Pesqueiro { get; set; }

        public ICollection<EspecieLago>? EspecieLagos { get; set; }
       
    }
}
