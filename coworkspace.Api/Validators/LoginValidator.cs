using FluentValidation;
using Coworkspace.Api.DTOs;

namespace Coworkspace.Api.Validators
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Se requiere un correo electrónico válido.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("La contraseña es obligatoria.");
        }
    }
}