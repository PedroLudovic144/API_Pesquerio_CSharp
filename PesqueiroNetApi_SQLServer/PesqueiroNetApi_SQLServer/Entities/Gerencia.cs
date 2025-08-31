namespace PesqueiroNetApi.Entities
{
    public class Gerencia
    {
        public int IdFuncionario { get; set; }
        public Funcionario? Funcionario { get; set; }

        public int IdEquipamentos { get; set; }
        public Equipamento? Equipamento { get; set; }
    }
}
