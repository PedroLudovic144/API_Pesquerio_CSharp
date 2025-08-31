using System;
using System.Text;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace PesqueiroNetApi.Utils
{
    public class Criptografia
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expiryHours;

        public Criptografia(IConfiguration config)
        {
            _key = config.GetValue<string>("Jwt:Key") ?? throw new ArgumentNullException("Jwt:Key");
            _issuer = config.GetValue<string>("Jwt:Issuer") ?? "PesqueiroApi";
            _audience = config.GetValue<string>("Jwt:Audience") ?? "PesqueiroApiClient";
            _expiryHours = config.GetValue<int>("Jwt:ExpiryHours"); 
            if (_expiryHours == 0) _expiryHours = 8;
        }

        public string HashSenha(string senha)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(senha);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool VerificarSenha(string senhaDigitada, string senhaHash)
        {
            if (senhaHash == null) return false;
            var hashDigitada = HashSenha(senhaDigitada);
            return hashDigitada == senhaHash;
        }

        public string GerarToken(string idUsuario, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, idUsuario),
                new Claim(ClaimTypes.Role, role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(_expiryHours),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
