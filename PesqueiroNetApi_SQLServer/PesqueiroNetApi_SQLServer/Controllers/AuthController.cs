using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PesqueiroNetApi.Data;
using PesqueiroNetApi.Utils;
using PesqueiroNetApi.Entities;

namespace PesqueiroNetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly Criptografia _crypto;

        public AuthController(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _crypto = new Criptografia(config);
        }

        [HttpPost("login-cliente")]
        public async Task<IActionResult> LoginCliente([FromForm] string email, [FromForm] string senha)
        {
            var cliente = await _db.Clientes.FirstOrDefaultAsync(c => c.EmailCliente == email);
            if (cliente == null) return Unauthorized(new { message = "Cliente não encontrado" });

            if (!_crypto.VerificarSenha(senha, cliente.SenhaCliente)) return Unauthorized(new { message = "Senha inválida" });

            var token = _crypto.GerarToken(cliente.IdCliente.ToString(), "Cliente");
            return Ok(new { token });
        }

        public class LoginDto { public string Nome { get; set; } = ""; public string Senha { get; set; } = ""; }

        [HttpPost("login-funcionario")]
        public async Task<IActionResult> LoginFuncionario([FromBody] LoginDto model)
        {
            var func = await _db.Funcionarios.FirstOrDefaultAsync(f => f.NomeFuncionario == model.Nome);
            if (func == null) return Unauthorized(new { message = "Funcionário não encontrado" });

            if (!_crypto.VerificarSenha(model.Senha, func.SenhaFuncionario)) return Unauthorized(new { message = "Senha inválida" });

            var token = _crypto.GerarToken(func.IdFuncionario.ToString(), "Funcionario");
            return Ok(new { token });
        }
    }
}
