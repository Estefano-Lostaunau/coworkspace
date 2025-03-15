using FluentValidation;
using Coworkspace.Api.DTOs;

namespace Coworkspace.Api.Validators
{
    public class ReservationValidator : AbstractValidator<ReservationDTO>
    {
        public ReservationValidator()
        {
            RuleFor(x => x.SpaceId).NotEmpty().WithMessage("El ID del espacio es obligatorio.");
            RuleFor(x => x.ReservationDate).NotEmpty().WithMessage("La fecha de la reserva es obligatoria.");
            RuleFor(x => x.StartTime).NotEmpty().WithMessage("La hora de inicio es obligatoria.");
            RuleFor(x => x.EndTime).NotEmpty().WithMessage("La hora de finalización es obligatoria.");
            RuleFor(x => x).Must(x => x.StartTime < x.EndTime).WithMessage("La hora de inicio debe ser antes de la hora de finalización.");
        }
    }
}