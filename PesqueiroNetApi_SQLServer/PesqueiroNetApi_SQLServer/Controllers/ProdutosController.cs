using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PesqueiroNetApi.Data;
using PesqueiroNetApi.Entities;

namespace PesqueiroNetApi_SQLServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        // Registrar Produto
        [HttpPost("registrar")]
        public IActionResult RegistrarProduto([FromBody] Produto produto)
        {
            // Verifica se o pesqueiro existe
            var pesqueiro = _context.Pesqueiros.Find(produto.IdPesqueiro);
            if (pesqueiro == null)
                return NotFound("Pesqueiro não encontrado. Informe um IdPesqueiro válido.");

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return Ok(new { message = "Produto registrado com sucesso!", produto });
        }

        // Retirar Produto (excluir do sistema)
        [HttpDelete("retirar/{id}")]
        public IActionResult RetirarProduto(int id)
        {
            var produto = _context.Produtos.Find(id);
            if (produto == null) return NotFound("Produto não encontrado.");

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(new { message = "Produto retirado do sistema com sucesso!", produto });
        }

        // Atualizar Produto
        [HttpPut("atualizar/{id}")]
        public IActionResult AtualizarProduto(int id, [FromBody] Produto produtoAtualizado)
        {
            var produto = _context.Produtos.Find(id);
            if (produto == null) return NotFound("Produto não encontrado.");

            produto.NomeProduto = produtoAtualizado.NomeProduto ?? produto.NomeProduto;
            produto.ValorProduto = produtoAtualizado.ValorProduto != 0 ? produtoAtualizado.ValorProduto : produto.ValorProduto;
            produto.QtdProduto = produtoAtualizado.QtdProduto != 0 ? produtoAtualizado.QtdProduto : produto.QtdProduto;
            produto.Fornecedor = produtoAtualizado.Fornecedor ?? produto.Fornecedor;

            _context.SaveChanges();

            return Ok(new { message = "Produto atualizado com sucesso!", produto });
        }

        // Consultar Estoque
        [HttpGet("estoque")]
        public IActionResult ConsultaEstoque()
        {
            var produtos = _context.Produtos
                .Select(p => new
                {
                    p.IdProduto,
                    p.NomeProduto,
                    p.QtdProduto,
                    p.ValorProduto,
                    p.IdPesqueiro
                })
                .ToList();

            return Ok(produtos);
        }

        // Atualizar Estoque (alterar a quantidade)
        [HttpPut("estoque/{id}")]
        public IActionResult AtualizarEstoque(int id, [FromQuery] int novaQuantidade)
        {
            var produto = _context.Produtos.Find(id);
            if (produto == null) return NotFound("Produto não encontrado.");

            produto.QtdProduto = novaQuantidade;
            _context.SaveChanges();

            return Ok(new { message = "Estoque atualizado com sucesso!", produto });
        }

        // Registrar Venda (diminui estoque)
        [HttpPost("venda/{id}")]
        public IActionResult RegistrarVenda(int id, [FromQuery] int quantidadeVendida)
        {
            var produto = _context.Produtos.Find(id);
            if (produto == null) return NotFound("Produto não encontrado.");

            if (produto.QtdProduto < quantidadeVendida)
                return BadRequest("Estoque insuficiente para a venda.");

            produto.QtdProduto -= quantidadeVendida;
            _context.SaveChanges();

            return Ok(new { message = "Venda registrada com sucesso!", produto });
        }
    }
}
