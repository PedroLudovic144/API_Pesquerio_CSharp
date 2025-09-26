using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using PesqueiroNetApi.Data;
using PesqueiroNetApi.Entities;

namespace PesqueiroNetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipamentoController : ControllerBase
    {
        private readonly AppDbContext _db;

        public EquipamentoController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _db.Equipamentos.ToListAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var e = await _db.Equipamentos
                .Where(x => x.IdEquipamentos == id)
                .Select(x => new {
                    x.IdEquipamentos,
                    x.NomeEquipamento,
                    x.Status,
                    x.QuantidadeEquipamento
                })
                .FirstOrDefaultAsync();

            if (e == null) return NotFound();
            return Ok(e);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Equipamento equipamento)
        {
            _db.Equipamentos.Add(equipamento);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = equipamento.IdEquipamentos }, equipamento);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Equipamento equipamento)
        {
            var existing = await _db.Equipamentos.FindAsync(id);
            if (existing == null) 
                return NotFound("Equipamento não encontrado.");

            // Atualiza apenas os campos enviados
            existing.NomeEquipamento = equipamento.NomeEquipamento ?? existing.NomeEquipamento;
            existing.QuantidadeEquipamento = equipamento.QuantidadeEquipamento != 0 
                ? equipamento.QuantidadeEquipamento 
                : existing.QuantidadeEquipamento;

            // Atualiza o status apenas se veio no body
            if (Enum.IsDefined(typeof(StatusEquipamento), equipamento.Status))
            {
                existing.Status = equipamento.Status;
            }

            await _db.SaveChangesAsync();
            return Ok(new 
            {
                Mensagem = "Equipamento atualizado com sucesso!",
                existing.IdEquipamentos,
                existing.NomeEquipamento,
                existing.QuantidadeEquipamento,
                Status = existing.Status.ToString()
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _db.Equipamentos.FindAsync(id);
            if (existing == null) return NotFound();
            _db.Equipamentos.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        // PATCH api/equipamento/{id}/status
        [HttpPatch("{id:int}/status")]
        public async Task<IActionResult> AlterarStatus(int id, [FromBody] StatusEquipamento novoStatus)
        {
            var equipamento = await _db.Equipamentos.FindAsync(id);
            if (equipamento == null) return NotFound("Equipamento não encontrado.");

            equipamento.Status = novoStatus;
            await _db.SaveChangesAsync();

            return Ok(new 
            { 
                Mensagem = "Status atualizado com sucesso!", 
                equipamento.IdEquipamentos, 
                equipamento.NomeEquipamento, 
                Status = equipamento.Status.ToString() 
            });
        }

    }
}
