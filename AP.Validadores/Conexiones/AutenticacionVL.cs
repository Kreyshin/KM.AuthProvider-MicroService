using AP.Aplicacion.Autenticacion.Dtos.Request;
using FluentValidation;

namespace AP.Validadores.Autenticacion
{
    public class AutenticacionVL : AbstractValidator<AutenticacionRQ>
    {
        public AutenticacionVL() {
            
            RuleFor(x => x.usuario)
                .NotEmpty().WithMessage("El usuario es requerido")
                .MaximumLength(50).WithMessage("El usuario no puede tener más de 50 caracteres");

            RuleFor(x => x.clave)
                .NotEmpty().WithMessage("La Clave es requerida")
                .MaximumLength(50).WithMessage("El usuario no puede tener más de 50 caracteres");

             RuleFor(x => x.cod_sistema)
                .NotEmpty().WithMessage("El codigo del sistema es requerido")
                .MaximumLength(50).WithMessage("El usuario no puede tener más de 50 caracteres");
        }
    }
}
