using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using PesqueiroNetApi.Data;
using PesqueiroNetApi.Entities;
using PesqueiroNetApi.Utils;
using System.Security.Claims;

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

        [HttpPost("{clienteId:int}/favoritar/{pesqueiroId:int}")]
        public async Task<IActionResult> Favoritar(int clienteId, int pesqueiroId)
        {
            var existe = await _db.Favoritos
                .AnyAsync(f => f.IdCliente == clienteId && f.IdPesqueiro == pesqueiroId);

            if (existe)
                return BadRequest("Este pesqueiro já está nos favoritos.");

            var favorito = new Favorito
            {
                IdCliente = clienteId,
                IdPesqueiro = pesqueiroId
            };

            _db.Favoritos.Add(favorito);
            await _db.SaveChangesAsync();

            return Ok("Pesqueiro favoritado com sucesso!");
        }
        [HttpDelete("{clienteId:int}/desfavoritar/{pesqueiroId:int}")]
        public async Task<IActionResult> Desfavoritar(int clienteId, int pesqueiroId)
        {
            var favorito = await _db.Favoritos
                .FirstOrDefaultAsync(f => f.IdCliente == clienteId && f.IdPesqueiro == pesqueiroId);

            if (favorito == null)
                return NotFound("Favorito não encontrado.");

            _db.Favoritos.Remove(favorito);
            await _db.SaveChangesAsync();

            return Ok("Pesqueiro removido dos favoritos.");
        }


        [HttpGet("favoritos")]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> ListarFavoritos()
        {
            var clienteId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var favoritos = await _db.Favoritos
                .Where(f => f.IdCliente == clienteId)
                .Include(f => f.Pesqueiro)
                .Select(f => new
                {
                    IdPesqueiro = f.Pesqueiro != null ? f.Pesqueiro.IdPesqueiro : 0,
                    Nome = f.Pesqueiro != null ? f.Pesqueiro.Nome : "Pesqueiro removido"
                })
                .ToListAsync();

            return Ok(favoritos);
        }
        // POST api/cliente/{clienteId}/comentar/{pesqueiroId}
        [HttpPost("{clienteId:int}/comentar/{pesqueiroId:int}")]
        public async Task<IActionResult> Comentar(int clienteId, int pesqueiroId, [FromBody] string texto)
        {
            // verifica se cliente e pesqueiro existem
            var cliente = await _db.Clientes.FindAsync(clienteId);
            var pesqueiro = await _db.Pesqueiros.FindAsync(pesqueiroId);

            if (cliente == null || pesqueiro == null)
                return NotFound("Cliente ou Pesqueiro não encontrado.");

            // cria o comentário
            var comentario = new Comentario
            {
                Texto = texto,
                DataComentario = DateTime.Now,
                IdPesqueiro = pesqueiroId
            };

            _db.Comentarios.Add(comentario);
            await _db.SaveChangesAsync();

            // cria o vínculo do cliente com o comentário
            var clienteComentario = new ClienteComentario
            {
                IdCliente = clienteId,
                IdComentario = comentario.IdComentario
            };

            _db.ClientesComentarios.Add(clienteComentario);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                Mensagem = "Comentário adicionado com sucesso!",
                Comentario = new
                {
                    comentario.IdComentario,
                    comentario.Texto,
                    comentario.DataComentario,
                    Pesqueiro = pesqueiro.Nome,
                    Cliente = cliente.NomeCliente
                }
            });
        }

        // GET api/cliente/pesqueiro/{pesqueiroId}/comentarios
        [HttpGet("pesqueiro/{pesqueiroId:int}/comentarios")]
        public async Task<IActionResult> ListarComentarios(int pesqueiroId)
        {
            var comentarios = await _db.Comentarios
                .Where(c => c.IdPesqueiro == pesqueiroId)
                .Include(c => c.ClienteComentarios!)
                    .ThenInclude(cc => cc.Cliente)
                .Select(c => new
                {
                    c.IdComentario,
                    c.Texto,
                    c.DataComentario,
                    Cliente = c.ClienteComentarios!.Select(cc => cc.Cliente!.NomeCliente).FirstOrDefault()
                })
                .ToListAsync();

            return Ok(comentarios);
        }

        // PUT api/cliente/{clienteId}/comentario/{comentarioId}
        [HttpPut("{clienteId:int}/comentario/{comentarioId:int}")]
        public async Task<IActionResult> EditarComentario(int clienteId, int comentarioId, [FromBody] string novoTexto)
        {
            var comentario = await _db.Comentarios
                .Include(c => c.ClienteComentarios!)
                .FirstOrDefaultAsync(c => c.IdComentario == comentarioId);

            if (comentario == null)
                return NotFound("Comentário não encontrado.");

            var pertenceAoCliente = comentario.ClienteComentarios!.Any(cc => cc.IdCliente == clienteId);
            if (!pertenceAoCliente)
                return Unauthorized("Você não pode editar um comentário de outro cliente.");

            comentario.Texto = novoTexto;
            await _db.SaveChangesAsync();

            return Ok(new { Mensagem = "Comentário editado com sucesso!", comentario.IdComentario, comentario.Texto });
        }
        
        // DELETE api/cliente/{clienteId}/comentario/{comentarioId}
        [HttpDelete("{clienteId:int}/comentario/{comentarioId:int}")]
        public async Task<IActionResult> DeletarComentario(int clienteId, int comentarioId)
        {
            var comentario = await _db.Comentarios
                .Include(c => c.ClienteComentarios!)
                .FirstOrDefaultAsync(c => c.IdComentario == comentarioId);

            if (comentario == null)
                return NotFound("Comentário não encontrado.");

            var pertenceAoCliente = comentario.ClienteComentarios!.Any(cc => cc.IdCliente == clienteId);
            if (!pertenceAoCliente)
                return Unauthorized("Você não pode excluir um comentário de outro cliente.");

            // remove vínculos na tabela associativa
            _db.ClientesComentarios.RemoveRange(comentario.ClienteComentarios!);

            // remove o comentário
            _db.Comentarios.Remove(comentario);

            await _db.SaveChangesAsync();

            return Ok("Comentário deletado com sucesso!");
        }
    }
}
