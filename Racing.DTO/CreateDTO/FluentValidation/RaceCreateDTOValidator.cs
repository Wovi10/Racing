using FluentValidation;
using Microsoft.Extensions.Localization;
using Racing.DTO.Resources;

namespace Racing.DTO.CreateDTO.FluentValidation
{
    public class RaceCreateDTOValidator : AbstractValidator<RaceCreateDTO>
    {
        public RaceCreateDTOValidator(IStringLocalizer<CreateDTOMessages> localizer)
        {
            RuleFor(race => race.Name)
                .NotEmpty()
                .WithMessage(localizer.GetString("name"));
            
            RuleFor(race => race.SeasonId)
                .NotEmpty()
                .WithMessage(localizer.GetString("season"));

            RuleFor(race => race.CircuitId)
                .NotEmpty()
                .WithMessage(localizer.GetString("circuit"));

            RuleFor(race => race.StartDate)
                .NotEmpty()
                .WithMessage(localizer.GetString("startdate"));

            RuleFor(race => race.EndDate)
                .NotEmpty()
                .WithMessage(localizer.GetString("enddate"));
        }
    }
}