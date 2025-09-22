using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PesqueiroNetApi.Data;
using PesqueiroNetApi.Entities;

namespace PesqueiroNetApi_SQLServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PesqueiroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PesqueiroController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("cadastrar-pesqueiro")]
        public async Task<IActionResult> CadastrarPesqueiro([FromBody] Pesqueiro pesqueiro)
        {
            if (pesqueiro == null)
            {
                return BadRequest("Dados do pesqueiro inválidos.");
            }

            _context.Pesqueiros.Add(pesqueiro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPesqueiroById), new { id = pesqueiro.IdPesqueiro }, pesqueiro);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pesqueiro>> GetPesqueiroById(int id)
        {
            var pesqueiro = await _context.Pesqueiros.FindAsync(id);

            if (pesqueiro == null)
            {
                return NotFound();
            }

            return pesqueiro;
        }

        [HttpPut("atualizar-pesqueiro/{id}")]
        public async Task<IActionResult> AtualizarPesqueiro(int id, [FromBody] Pesqueiro pesqueiro)
        {
            if (id != pesqueiro.IdPesqueiro)
            {
                return BadRequest("ID do pesqueiro não corresponde.");
            }

            _context.Entry(pesqueiro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PesqueiroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool PesqueiroExists(int id)
        {
            return _context.Pesqueiros.Any(e => e.IdPesqueiro == id);
        }

        [HttpPost("cadastrar-funcionario")]
        public async Task<IActionResult> CadastrarFuncionario([FromBody] Funcionario funcionario)
        {
            if (funcionario == null)
            {
                return BadRequest("Dados do funcionário inválidos.");
            }

            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFuncionarioById), new { id = funcionario.IdFuncionario }, funcionario);
        }

        [HttpGet("funcionario/{id}")]
        public async Task<ActionResult<Funcionario>> GetFuncionarioById(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return funcionario;
        }

        [HttpPut("atualizar-funcionario/{id}")]
        public async Task<IActionResult> AtualizarFuncionario(int id, [FromBody] Funcionario funcionario)
        {
            if (id != funcionario.IdFuncionario)
            {
                return BadRequest("ID do funcionário não corresponde.");
            }

            _context.Entry(funcionario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionarios.Any(e => e.IdFuncionario == id);
        }

        [HttpGet("relatorio-pesqueiros")]
        public async Task<ActionResult<IEnumerable<Pesqueiro>>> GetRelatorioPesqueiros()
        {
            // You can customize this to include related data using .Include()
            var pesqueiros = await _context.Pesqueiros
                .Include(p => p.Comentarios)
                .Include(p => p.Favoritos)
                .ToListAsync();

            if (!pesqueiros.Any())
            {
                return NotFound("Nenhum pesqueiro encontrado para o relatório.");
            }

            return pesqueiros;
        }

        [HttpGet("relatorio-funcionarios")]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetRelatorioFuncionarios()
        {
            var funcionarios = await _context.Funcionarios.ToListAsync();

            if (!funcionarios.Any())
            {
                return NotFound("Nenhum funcionário encontrado para o relatório.");
            }

            return funcionarios;
        }

        public async Task<IActionResult> GerarRelatorioConsolidado()
        {
            // 1. Obter todos os aluguéis
            var alugueis = await _context.Alugueis.ToListAsync();

            // 2. Obter todas as compras (itens de venda)
            var compras = await _context.Compras.ToListAsync();

            // 3. Obter o estoque de produtos
            var produtos = await _context.Produtos.ToListAsync();

            // 4. Obter a quantidade de peixes em cada lago
            var peixesNosLagos = await _context.EspeciesLagos
                .Include(el => el.Especie)
                .Include(el => el.Lago)
                .Select(el => new 
                {
                    Lago = el.Lago!.NomeLago,
                    Especie = el.Especie!.NomeEspecie,
                    Quantidade = el.QtdPeixes
                })
                .ToListAsync();

            // Criar um objeto anônimo para agrupar todos os dados do relatório
            var relatorio = new
            {
                DataGeracao = DateTime.Now,
                Alugueis = alugueis,
                Compras = compras,
                EstoqueProdutos = produtos,
                PeixesPorLago = peixesNosLagos
            };

            return Ok(relatorio);
        }
    
    }
}