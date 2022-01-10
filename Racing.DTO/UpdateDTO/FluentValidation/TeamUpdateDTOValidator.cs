using FluentValidation;
using Microsoft.Extensions.Localization;
using Racing.DTO.Resources;

namespace Racing.DTO.UpdateDTO.FluentValidation
{
    public class TeamUpdateDTOValidator : AbstractValidator<TeamUpdateDTO>
    {
        public TeamUpdateDTOValidator(IStringLocalizer<CreateDTOMessages> localizer)
        {
            RuleFor(team => team.Name)
                .NotEmpty()
                .WithMessage(localizer.GetString("name"));
        }
    }
}