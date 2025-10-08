using System;

namespace PesqueiroNetApi.Entities
{
    public class Peixe
    {
        public int IdPeixe { get; set; }
        public string? Especie { get; set; }
        public double? PesoMedio { get; set; }
        public int IdLago { get; set; }

        public Lago? Lago { get; set; }

    }
}
