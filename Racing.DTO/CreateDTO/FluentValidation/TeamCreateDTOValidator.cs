using FluentValidation;
using Microsoft.Extensions.Localization;
using Racing.DTO.Resources;

namespace Racing.DTO.CreateDTO.FluentValidation
{
    public class TeamCreateDTOValidator : AbstractValidator<TeamCreateDTO>
    {
        public TeamCreateDTOValidator(IStringLocalizer<CreateDTOMessages> localizer)
        {
            RuleFor(team => team.Name)
                .NotEmpty()
                .WithMessage(localizer.GetString("name"));
        }
    }
}