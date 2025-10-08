using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PesqueiroNetApi.Data;
using PesqueiroNetApi.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesqueiroNetApi_SQLServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeixesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public PeixesController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Peixe>>> GetAll()
        {
            return await _db.Peixe.Include(p => p.Lago).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Peixe>> GetById(int id)
        {
            var peixe = await _db.Peixe.Include(p => p.Lago)
                                        .FirstOrDefaultAsync(p => p.IdPeixe == id);
            if (peixe == null)
                return NotFound();

            return peixe;
        }

        [HttpPost]
        public async Task<ActionResult<Peixe>> Create(Peixe peixe)
        {
            var lago = await _db.Lago.FindAsync(peixe.IdLago);
            if (lago == null)
                return BadRequest("Lago não encontrado.");

            _db.Peixe.Add(peixe);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = peixe.IdPeixe }, peixe);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Peixe peixe)
        {
            if (id != peixe.IdPeixe)
                return BadRequest();

            var existente = await _db.Peixe.FindAsync(id);
            if (existente == null)
                return NotFound();

            existente.Especie = peixe.Especie;
            existente.PesoMedio = peixe.PesoMedio;
            existente.IdLago = peixe.IdLago;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var peixe = await _db.Peixe.FindAsync(id);
            if (peixe == null)
                return NotFound();

            _db.Peixe.Remove(peixe);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
