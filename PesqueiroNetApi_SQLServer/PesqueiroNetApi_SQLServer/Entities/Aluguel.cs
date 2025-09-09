using System;

namespace PesqueiroNetApi.Entities
{
    public class Aluguel
    {
        public int IdAluguel { get; set; }
        public decimal ValorAluguel { get; set; }
        public DateTime DataHoraRetirada { get; set; }
        public DateTime? DataHoraDevolucao { get; set; }
        public StatusAluguel Status { get; set; } = StatusAluguel.Cancelado;
        public string? Observacao { get; set; }
        public int Quantidade { get; set; }

        // Relacionamento com Equipamento
        public int IdEquipamento { get; set; }
        public Equipamento? Equipamento { get; set; }

        // Relacionamento com Funcionário
        public int IdFuncionario { get; set; }
        public Funcionario? Funcionario { get; set; }
    }
}
