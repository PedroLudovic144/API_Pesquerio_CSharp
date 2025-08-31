using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using PesqueiroNetApi.Data;
using PesqueiroNetApi.Entities;
using PesqueiroNetApi.Utils;

namespace PesqueiroNetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly Criptografia _crypto;

        public FuncionarioController(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _crypto = new Criptografia(config);
        }

        [HttpGet]
        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> GetAll() => Ok(await _db.Funcionarios.ToListAsync());

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> GetById(int id)
        {
            var f = await _db.Funcionarios.FindAsync(id);
            if (f == null) return NotFound();
            return Ok(f);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Funcionario funcionario)
        {
            if (!string.IsNullOrEmpty(funcionario.SenhaFuncionario))
                funcionario.SenhaFuncionario = _crypto.HashSenha(funcionario.SenhaFuncionario);

            _db.Funcionarios.Add(funcionario);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = funcionario.IdFuncionario }, funcionario);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> Update(int id, [FromBody] Funcionario funcionario)
        {
            var existing = await _db.Funcionarios.FindAsync(id);
            if (existing == null) return NotFound();

            existing.NomeFuncionario = funcionario.NomeFuncionario ?? existing.NomeFuncionario;
            if (!string.IsNullOrEmpty(funcionario.SenhaFuncionario))
                existing.SenhaFuncionario = _crypto.HashSenha(funcionario.SenhaFuncionario);

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _db.Funcionarios.FindAsync(id);
            if (existing == null) return NotFound();
            _db.Funcionarios.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
