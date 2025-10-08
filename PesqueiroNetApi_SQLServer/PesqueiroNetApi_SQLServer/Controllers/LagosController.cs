using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PesqueiroNetApi.Data;
using PesqueiroNetApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PesqueiroNetApi_SQLServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LagosController : ControllerBase
    {
         private readonly AppDbContext _context;

        public LagosController(AppDbContext db)
        {
            _context = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lago>>> GetAll()
        {
            return await _context.Lago.Include(l => l.Pesqueiro).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lago>> GetById(int id)
        {
            var lago = await _context.Lago.Include(l => l.Pesqueiro)
                                           .FirstOrDefaultAsync(l => l.IdLago == id);
            if (lago == null)
                return NotFound();

            return lago;
        }

        [HttpPost]
        public async Task<ActionResult<Lago>> Create(Lago lago)
        {
            var pesqueiro = await _context.Pesqueiros.FindAsync(lago.IdPesqueiro);
            if (pesqueiro == null)
                return BadRequest("Pesqueiro não encontrado.");

            _context.Lago.Add(lago);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = lago.IdLago }, lago);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Lago lago)
        {
            if (id != lago.IdLago)
                return BadRequest();

            var existente = await _context.Lago.FindAsync(id);
            if (existente == null)
                return NotFound();

            existente.NomeLago = lago.NomeLago;
            existente.TamanhoLago = lago.TamanhoLago;
            existente.IdPesqueiro = lago.IdPesqueiro;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var lago = await _context.Lago.FindAsync(id);
            if (lago == null)
                return NotFound();

            _context.Lago.Remove(lago);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
