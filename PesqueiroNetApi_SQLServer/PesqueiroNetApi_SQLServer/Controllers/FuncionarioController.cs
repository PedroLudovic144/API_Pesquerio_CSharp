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
        public async Task<IActionResult> GetAll() => Ok(await _db.Funcionarios.ToListAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var f = await _db.Funcionarios.FindAsync(id);
            if (f == null) return NotFound();
            return Ok(f);
        }

        // DTO para criar funcionário
        public class FuncionarioCreateDto
        {
            public string NomeFuncionario { get; set; } = "";
            public string SenhaFuncionario { get; set; } = "";
            public int IdPesqueiro { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FuncionarioCreateDto model)
        {
            if (string.IsNullOrEmpty(model.NomeFuncionario) || string.IsNullOrEmpty(model.SenhaFuncionario))
                return BadRequest(new { message = "Nome e senha são obrigatórios" });

            var pesqueiro = await _db.Pesqueiros.FindAsync(model.IdPesqueiro);
            if (pesqueiro == null) return BadRequest(new { message = "Pesqueiro não existe" });

            var funcionario = new Funcionario
            {
                NomeFuncionario = model.NomeFuncionario,
                SenhaFuncionario = _crypto.HashSenha(model.SenhaFuncionario),
                IdPesqueiro = model.IdPesqueiro
            };

            _db.Funcionarios.Add(funcionario);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = funcionario.IdFuncionario }, funcionario);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] FuncionarioCreateDto model)
        {
            var existing = await _db.Funcionarios.FindAsync(id);
            if (existing == null) return NotFound();

            if (!string.IsNullOrEmpty(model.NomeFuncionario))
                existing.NomeFuncionario = model.NomeFuncionario;

            if (!string.IsNullOrEmpty(model.SenhaFuncionario))
                existing.SenhaFuncionario = _crypto.HashSenha(model.SenhaFuncionario);

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
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
