using AP.Dominio.Comunes;
using AP.Dominio.Entidades;
using AP.Dominio.Interfaces;
using AP.Infraestructura.Persistencia;
using Microsoft.Extensions.Logging;
using AP.Infraestructura.Comunes;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AP.Infraestructura.Repositorios.Querys
{
    public class UsuariosRepositoryQ(DbConexion dbConexion, ILogger<UsuariosRepositoryQ> logger) : IUsuariosRepositoryQ
    {
        private readonly DbConexion _dbConexion = dbConexion;
        private readonly ILogger<UsuariosRepositoryQ> _logger = logger;

        public async Task<SingleResponse<UsuariosEN>> Auth(UsuariosEN usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            var oResp = new SingleResponse<UsuariosEN>();

            var objParam = Utilitarios.GenerarParametros(new
            {
                IC_Usuario = usuario.C_Usuario,
                IC_Cod_Sistema = usuario.C_Cod_Sistema
            });

            try
            {
                using var connection = _dbConexion.CrearConexion;
                oResp.Data = await connection.QuerySingleOrDefaultAsync<UsuariosEN>(
                         sql: "Sp_UsuarioQ_Autenticar",
                         commandType: CommandType.StoredProcedure,
                         param: objParam
                       );
            }
            catch (SqlException exsql)
            {
                if (exsql.Number == 50001)
                {
                    // Es un mensaje de validación del SP, no un error crítico
                    _logger.LogWarning("Validación de negocio: {Mensaje}", exsql.Message);
                    oResp.StatusMessage = exsql.Message;
                    oResp.StatusType = "VALIDATION";
                }
                else
                {
                    // Es un error real de SQL
                    _logger.LogError(exsql, "SQL Error ({ErrorCode}): Ocurrió una excepción SQL al autenticar usuario.", exsql.Number);
                    oResp.ErrorCode = exsql.Number;
                    oResp.ErrorMessage = "Error de base de datos, contactar con el administrador del sistema.";
                    oResp.StatusType = "SQL-ERROR";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Backend Error (50100): Ocurrió una excepción en C# al autenticar usuario.");
                oResp.ErrorCode = 50100;
                oResp.ErrorMessage = "Error de BackEnd, comunicarse con el encargado de este microservicio.";
                oResp.StatusType = "BACKEND-ERROR";
            }
            return oResp;
        }
    }    
}
