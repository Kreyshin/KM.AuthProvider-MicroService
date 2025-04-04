using AP.Aplicacion.Conexiones.Dtos.Request;
using AP.Aplicacion.Conexiones.Interfaces;
using AP.Dominio.Comunes;
using AP.Dominio.Entidades;
using AP.Dominio.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Aplicacion.Conexiones.CasosUso
{
    public class GestionConexionesCU(
        IConexionRepositoryC conexionRepoC, 
        IMapper imapper,
        ILogger<GestionConexionesCU> logger) : IGestionConexionesCU
    {
        private readonly IConexionRepositoryC _conexionRepoC = conexionRepoC;
        private readonly IMapper _imapper = imapper;
        private readonly ILogger<GestionConexionesCU> _logger = logger;

        public async Task<SingleResponse<int>> GuardarConexion(NuevaConexionRQ conexionRQ)
        {
            if(conexionRQ == null)
            {
                throw new ArgumentNullException(nameof(conexionRQ));
            }

            try
            {
                var conexionEN = _imapper.Map<ConexionesEN>(conexionRQ);
                conexionEN.C_Usuario_Creacion = "SISTEMAS";
                
                var oRes = await _conexionRepoC.GuardarConexion(conexionEN);


                if(oRes.ErrorCode == 500001 || oRes.ErrorCode == 0){
                    return new SingleResponse<int>
                    {
                        StatusCode = 200,
                        StatusMessage = oRes.StatusMessage,
                        Data = 1
                    };
                }
                else
                {
                    return new SingleResponse<int>
                    {
                        StatusCode = 500,
                        Data = 0,
                        StatusMessage = oRes.ErrorMessage,
                        StatusType = oRes.StatusType
                    };
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{50200}: Ocurrio un exepcion(c#) al intentar crear el rol.");
                return new SingleResponse<int>
                {
                    StatusCode = 500,
                    StatusType = "BACKEND-ERROR",
                    StatusMessage = "Error de BackEnd, comunicarse con el encargado de este microservicio."
                };
            }
        }
    }
}
