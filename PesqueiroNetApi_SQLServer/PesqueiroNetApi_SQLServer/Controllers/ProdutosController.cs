using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PesqueiroNetApi_SQLServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PesqueiroController(AppDbContext context)
        {
           _context = context;
        }
private static List<Produto> _produtos = new List<Produto>();

        // Registrar Produto
        [HttpPost("registrar")]
        public IActionResult RegistrarProduto([FromBody] Produto produto)
        {
            produto.IdProduto = _produtos.Count > 0 ? _produtos.Max(p => p.IdProduto) + 1 : 1;
            _produtos.Add(produto);
            return Ok(new { message = "Produto registrado com sucesso!", produto });
        }

        // Retirar Produto (excluir do sistema)
        [HttpDelete("retirar/{id}")]
        public IActionResult RetirarProduto(int id)
        {
            var produto = _produtos.FirstOrDefault(p => p.IdProduto == id);
            if (produto == null) return NotFound("Produto não encontrado.");

            _produtos.Remove(produto);
            return Ok(new { message = "Produto retirado do sistema com sucesso!", produto });
        }

        // Atualizar Produto
        [HttpPut("atualizar/{id}")]
        public IActionResult AtualizarProduto(int id, [FromBody] Produto produtoAtualizado)
        {
            var produto = _produtos.FirstOrDefault(p => p.IdProduto == id);
            if (produto == null) return NotFound("Produto não encontrado.");

            produto.NomeProduto = produtoAtualizado.NomeProduto ?? produto.NomeProduto;
            produto.ValorProduto = produtoAtualizado.ValorProduto != 0 ? produtoAtualizado.ValorProduto : produto.ValorProduto;
            produto.QtdProduto = produtoAtualizado.QtdProduto != 0 ? produtoAtualizado.QtdProduto : produto.QtdProduto;
            produto.Fornecedor = produtoAtualizado.Fornecedor ?? produto.Fornecedor;

            return Ok(new { message = "Produto atualizado com sucesso!", produto });
        }

        // Consultar Estoque
        [HttpGet("estoque")]
        public IActionResult ConsultaEstoque()
        {
            return Ok(_produtos.Select(p => new
            {
                p.IdProduto,
                p.NomeProduto,
                p.QtdProduto,
                p.ValorProduto
            }));
        }

        // Atualizar Estoque (alterar a quantidade)
        [HttpPut("estoque/{id}")]
        public IActionResult AtualizarEstoque(int id, [FromQuery] int novaQuantidade)
        {
            var produto = _produtos.FirstOrDefault(p => p.IdProduto == id);
            if (produto == null) return NotFound("Produto não encontrado.");

            produto.QtdProduto = novaQuantidade;
            return Ok(new { message = "Estoque atualizado com sucesso!", produto });
        }

        // Registrar Venda (diminui estoque)
        [HttpPost("venda/{id}")]
        public IActionResult RegistrarVenda(int id, [FromQuery] int quantidadeVendida)
        {
            var produto = _produtos.FirstOrDefault(p => p.IdProduto == id);
            if (produto == null) return NotFound("Produto não encontrado.");

            if (produto.QtdProduto < quantidadeVendida)
                return BadRequest("Estoque insuficiente para a venda.");

            produto.QtdProduto -= quantidadeVendida;
            return Ok(new { message = "Venda registrada com sucesso!", produto });
        }
    }
}