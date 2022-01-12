using FluentValidation;
using Microsoft.Extensions.Localization;
using Racing.DTO.Resources;

namespace Racing.DTO.CreateDTO.FluentValidation
{
    public class  PilotCreateDTOValidator : AbstractValidator<PilotCreateDTO>
    {
        public PilotCreateDTOValidator(IStringLocalizer<CreateDTOMessages> localizer)
        {
            // Required fields
            RuleFor(pilot => pilot.Name)
                .NotEmpty()
                .WithMessage(localizer.GetString("name"));

            RuleFor(pilot => pilot.Birthdate)
                .NotEmpty()
                .WithMessage(localizer.GetString("birthdate"));

            RuleFor(pilot => pilot.Gender)
                .NotEmpty()
                .WithMessage(localizer.GetString("gender"));

            RuleFor(pilot => pilot.FirstName)
                .NotEmpty()
                .WithMessage(localizer.GetString("firstname"));
            
            RuleFor(pilot => pilot.NickName)
                .NotEmpty()
                .WithMessage(localizer.GetString("nickname"));
            
            RuleFor(pilot => pilot.LicensNr)
                .NotEmpty()
                .WithMessage(localizer.GetString("licensenr"));
            
            // Value rules
            RuleFor(pilot => pilot.LicensNr)
                .Matches("^[0-9A-F]12$")
                .WithMessage(localizer.GetString("licensenrForm"));

            RuleFor(pilot => pilot.Length)
                .LessThanOrEqualTo(200).Empty()
                .When(pilot => pilot.Length != 0)
                .WithMessage(localizer.GetString("length"));
        }
    }
}