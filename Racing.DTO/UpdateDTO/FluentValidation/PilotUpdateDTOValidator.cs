using FluentValidation;
using Microsoft.Extensions.Localization;
using Racing.DTO.Resources;

namespace Racing.DTO.UpdateDTO.FluentValidation
{
    public class PilotUpdateDTOValidator : AbstractValidator<PilotUpdateDTO>
    {
        public PilotUpdateDTOValidator(IStringLocalizer<CreateDTOMessages> localizer)
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
                .Matches("^[0-9A-F]+$")
                .WithMessage(localizer.GetString("licensenrForm"))
                .Length(12)
                .WithMessage(localizer.GetString("licensenrLength"));

            RuleFor(pilot => pilot.Length)
                .LessThanOrEqualTo(200)
                .When(pilot => !string.IsNullOrEmpty(pilot.Length.ToString()))
                .WithMessage(localizer.GetString("length"));
        }
    }
}