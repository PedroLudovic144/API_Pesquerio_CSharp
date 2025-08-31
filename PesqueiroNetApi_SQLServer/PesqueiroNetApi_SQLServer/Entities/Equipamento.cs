namespace PesqueiroNetApi.Entities
{
    public class Equipamento
    {
        public int IdEquipamentos { get; set; }
        public string? NomeEquipamento { get; set; }
        public bool EquipamentoEmUso { get; set; }
        public int QuantidadeEquipamento { get; set; }

        public ICollection<Gerencia>? Gerencias { get; set; }
        public ICollection<Aluguel>? Alugueis { get; set; }
    }
}
