using AP.Aplicacion.Autenticacion.Dtos.Request;
using AP.Aplicacion.Autenticacion.Dtos.Response;
using AP.Aplicacion.Autenticacion.Interfaces;
using AP.Dominio.Comunes;
using AP.Dominio.Entidades;
using AP.Dominio.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Aplicacion.Autenticacion.CasosUso
{
    public class AutenticacionCU(IUsuariosRepositoryQ usuariosRepository, IMapper imapper) : IAutenticacionCU
    {
        private readonly IUsuariosRepositoryQ _usuariosRepository = usuariosRepository;
        private readonly IMapper _imapper = imapper;
        public async Task<SingleResponse<AutenticacionRS>> Auth(AutenticacionRQ authRQ)
        {
            if (authRQ == null)
            {
                throw new ArgumentException(nameof(authRQ));
            }

            try
            {
                var usuariosEN = _imapper.Map<UsuariosEN>(authRQ);
                var oRes = await _usuariosRepository.Auth(usuariosEN);

                if (oRes.ErrorCode == 0)
                {
                    bool isPasswordValid = BCrypt.Net.BCrypt.Verify(authRQ.clave, oRes.Data.C_Clave);
                    if (!isPasswordValid)
                    {
                        oRes.StatusCode = 401;
                        oRes.StatusMessage = "Usuario o Clave incorrecta.";
                    };
                }
                else if (oRes.ErrorCode == 50001)
                {
                    oRes.StatusCode = 401;
                }

                var oResp = new SingleResponse<AutenticacionRS>
                {
                    StatusCode = oRes.StatusCode,
                    Data = oRes.StatusCode == 0 ? _imapper.Map<AutenticacionRS>(oRes.Data) : null,
                    StatusMessage = oRes.StatusMessage
                };

                return oResp;
            }
            catch (Exception ex)
            {
                return new SingleResponse<AutenticacionRS>
                {
                    StatusCode = -1,
                    StatusMessage = ex.Message
                };
            }
        }
    }
}
