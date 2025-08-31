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
            var e = await _db.Equipamentos.FindAsync(id);
            if (e == null) return NotFound();
            return Ok(e);
        }

        [HttpPost]
        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> Create([FromBody] Equipamento equipamento)
        {
            _db.Equipamentos.Add(equipamento);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = equipamento.IdEquipamentos }, equipamento);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> Update(int id, [FromBody] Equipamento equipamento)
        {
            var existing = await _db.Equipamentos.FindAsync(id);
            if (existing == null) return NotFound();
            existing.NomeEquipamento = equipamento.NomeEquipamento ?? existing.NomeEquipamento;
            existing.EquipamentoEmUso = equipamento.EquipamentoEmUso;
            existing.QuantidadeEquipamento = equipamento.QuantidadeEquipamento;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _db.Equipamentos.FindAsync(id);
            if (existing == null) return NotFound();
            _db.Equipamentos.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
