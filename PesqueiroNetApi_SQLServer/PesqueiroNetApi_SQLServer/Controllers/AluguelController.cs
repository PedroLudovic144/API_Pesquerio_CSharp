using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using PesqueiroNetApi.Data;
using PesqueiroNetApi.Entities;
using System.Security.Claims;



namespace PesqueiroNetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AluguelController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AluguelController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> CriarAluguel([FromBody] Aluguel aluguel)
        {
            // pega o id do funcionário logado no token
            var funcionarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // valida equipamento
            var equipamento = await _db.Equipamentos.FindAsync(aluguel.IdEquipamento);
            if (equipamento == null) return NotFound("Equipamento não encontrado.");

            if (equipamento.QuantidadeEquipamento < aluguel.Quantidade)
                return BadRequest("Quantidade solicitada indisponível.");

            // cria aluguel
            aluguel.IdFuncionario = funcionarioId;
            aluguel.DataHoraRetirada = DateTime.UtcNow;
            aluguel.Status = StatusAluguel.Ativo;

            _db.Alugueis.Add(aluguel);

            // atualiza estoque do equipamento
            equipamento.QuantidadeEquipamento -= aluguel.Quantidade;

            await _db.SaveChangesAsync();

            return Ok(new
            {
                Mensagem = "Aluguel registrado com sucesso!",
                aluguel.IdAluguel,
                aluguel.IdEquipamento,
                equipamento.NomeEquipamento,
                aluguel.Quantidade,
                aluguel.IdFuncionario,
                aluguel.DataHoraRetirada,
                aluguel.DataHoraDevolucao
            });
        }
    }
}
