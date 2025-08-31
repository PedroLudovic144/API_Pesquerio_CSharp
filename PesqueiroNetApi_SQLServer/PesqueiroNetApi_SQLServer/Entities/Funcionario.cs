namespace PesqueiroNetApi.Entities
{
    public class Funcionario
    {
        public int IdFuncionario { get; set; }
        public string? NomeFuncionario { get; set; }
        public string? SenhaFuncionario { get; set; }

        public ICollection<Gerencia>? Gerencias { get; set; }
    }
}
