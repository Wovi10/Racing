using FluentValidation;
using Microsoft.Extensions.Localization;
using Racing.DTO.Resources;


namespace Racing.DTO.CreateDTO.FluentValidation
{
    public class CircuitCreateDTOValidator : AbstractValidator<CircuitCreateDTO>
    {
        public CircuitCreateDTOValidator(IStringLocalizer<CreateDTOMessages> localizer)
        {
            RuleFor(circuit => circuit.Name)
                .NotEmpty()
                .WithMessage(localizer.GetString("name"));
            
            RuleFor(circuit => circuit.Length)
                .NotEmpty()
                .WithMessage(localizer.GetString("length"));

            RuleFor(circuit => circuit.CountryId)
                .NotEmpty()
                .WithMessage(localizer.GetString("country"));

            RuleFor(circuit => circuit.State)
                .NotEmpty()
                .WithMessage(localizer.GetString("state"));

            RuleFor(circuit => circuit.Street)
                .NotEmpty()
                .WithMessage(localizer.GetString("street"));

            RuleFor(circuit => circuit.Number)
                .NotEmpty()
                .WithMessage(localizer.GetString("number"));
        }
    }
}