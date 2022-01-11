using FluentValidation;
using Microsoft.Extensions.Localization;
using Racing.DTO.Resources;

namespace Racing.DTO.UpdateDTO.FluentValidation
{
    public class RaceUpdateDTOValidator : AbstractValidator<RaceUpdateDTO>
    {
        public RaceUpdateDTOValidator(IStringLocalizer<CreateDTOMessages> localizer)
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

            RuleFor(race => race.StartDate)
                .LessThanOrEqualTo(race => race.EndDate)
                .WithMessage(localizer.GetString("wrongdate"));

            RuleFor(race => race.EndDate)
                .NotEmpty()
                .WithMessage(localizer.GetString("enddate"));
        }
    }
}