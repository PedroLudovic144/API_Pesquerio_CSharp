using System;

namespace PesqueiroNetApi.Entities
{
    public class PeixeCapturado
    {
        public int IdPeixeCapturado { get; set; }
        public decimal Peso { get; set; }
        public DateOnly DataPeixeCapturado { get; set; }

        public int IdEspecie { get; set; }
        public Especie? Especie { get; set; }

        public ICollection<PeixeCliente>? PeixeClientes { get; set; }
    }
}
