using AP.Aplicacion.Autenticacion.Dtos.Request;
using AP.Aplicacion.Autenticacion.Interfaces;
using AP.Aplicacion.Conexiones.Dtos.Request;
using AP.Aplicacion.Conexiones.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AP.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IAutenticacionCU _authCU;
        private readonly IGestionConexionesCU _GestionConexionesCU;
        private readonly IConfiguration _configuration;

        public AutenticacionController(IConfiguration configuration, IAutenticacionCU authCU, IGestionConexionesCU GestionConexionesCU)
        {
            _configuration = configuration;
            _authCU = authCU;
            _GestionConexionesCU = GestionConexionesCU;
        }


        [HttpPost("token")]
        public async Task<IActionResult> Login([FromBody] AutenticacionRQ usuario)
        {
            if (usuario is null)
                return BadRequest(new { StatusCode = 400, StatusType = "InvalidInput", StatusMessage = "El cuerpo de la peticion no puede estar vacio." });

            var oResult = await _authCU.Auth(usuario);
            if (oResult.StatusCode != 0)
                return oResult.StatusCode switch
                {
                    401 => Unauthorized(new { oResult.StatusMessage }),
                    _ => StatusCode(500, new { oResult.StatusCode, oResult.StatusMessage })
                };

            var token = GenerarJwtToken(usuario.usuario, oResult.Data.IdUsuario);
            var refreshToken = GenerarRefreshToken();

            await _GestionConexionesCU.GuardarConexion(new NuevaConexionRQ
            {
                idUsuario = oResult.Data.IdUsuario,
                idRol = oResult.Data.IdRol,
                codSistema = usuario.cod_sistema,
                refreshToken = refreshToken
            });

            return Ok(new { Usuario = oResult.Data, Token = token, RefreshToken = refreshToken });
        }


        private string GenerarJwtToken(string NombreUsuario, int IdUsuario)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Información de usuario y claims (roles, etc.)
            var claims = new[]
            {
                new Claim("UName", NombreUsuario),
                new Claim("UId", IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerarRefreshToken()
        {
            var randomNumber = new byte[32];

            RandomNumberGenerator.Create().GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

    }
}
