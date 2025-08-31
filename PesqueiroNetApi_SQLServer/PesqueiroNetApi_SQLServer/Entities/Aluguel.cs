using System;

namespace PesqueiroNetApi.Entities
{
    public class Aluguel
    {
        public int IdAluguel { get; set; }
        public decimal ValorAluguel { get; set; }
        public DateTime DataHoraRetirada { get; set; }
        public DateTime? DataHoraDevolucao { get; set; }
        public string? Observacao { get; set; }

        public int IdEquipamentos { get; set; }
        public Equipamento? Equipamento { get; set; }

        public ICollection<AluguelCliente>? AluguelClientes { get; set; }
    }
}
