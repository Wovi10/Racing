using FluentValidation;
using Microsoft.Extensions.Localization;
using Racing.DTO.Resources;

namespace Racing.DTO.UpdateDTO.FluentValidation
{
    public class PilotUpdateDTOValidator : AbstractValidator<PilotUpdateDTO>
    {
        public PilotUpdateDTOValidator(IStringLocalizer<CreateDTOMessages> localizer)
        {
            RuleFor(pilot => pilot.Name)
                .NotEmpty()
                .WithMessage(localizer.GetString("name"));
            
            RuleFor(pilot => pilot.FirstName)
                .NotEmpty()
                .WithMessage(localizer.GetString("firstname"));
            
            RuleFor(pilot => pilot.NickName)
                .NotEmpty()
                .WithMessage(localizer.GetString("nickname"));
            
            RuleFor(pilot => pilot.Length)
                .NotEmpty()
                .WithMessage(localizer.GetString("length"));

            RuleFor(pilot => pilot.Weight)
                .NotEmpty()
                .WithMessage(localizer.GetString("weight"));

            RuleFor(pilot => pilot.Birthdate)
                .NotEmpty()
                .WithMessage(localizer.GetString("birthdate"));

            RuleFor(pilot => pilot.Gender)
                .NotEmpty()
                .WithMessage(localizer.GetString("gender"));
            
            RuleFor(pilot => pilot.PhotoPath)
                .NotEmpty()
                .WithMessage(localizer.GetString("photopath"));
        }
    }
}