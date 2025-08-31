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
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly Criptografia _crypto;

        public ClienteController(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _crypto = new Criptografia(config);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _db.Clientes.ToListAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var c = await _db.Clientes.FindAsync(id);
            if (c == null) return NotFound();
            return Ok(c);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            if (!string.IsNullOrEmpty(cliente.SenhaCliente))
                cliente.SenhaCliente = _crypto.HashSenha(cliente.SenhaCliente);

            _db.Clientes.Add(cliente);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = cliente.IdCliente }, cliente);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] Cliente cliente)
        {
            var existing = await _db.Clientes.FindAsync(id);
            if (existing == null) return NotFound();

            existing.NomeCliente = cliente.NomeCliente ?? existing.NomeCliente;
            existing.EmailCliente = cliente.EmailCliente ?? existing.EmailCliente;
            if (!string.IsNullOrEmpty(cliente.SenhaCliente))
                existing.SenhaCliente = _crypto.HashSenha(cliente.SenhaCliente);

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _db.Clientes.FindAsync(id);
            if (existing == null) return NotFound();
            _db.Clientes.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
