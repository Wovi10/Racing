using FluentValidation;
using Microsoft.Extensions.Localization;
using Racing.DTO.Resources;

namespace Racing.DTO.UpdateDTO.FluentValidation
{
    public class SeasonUpdateDTOValidator : AbstractValidator<SeasonUpdateDTO>
    {
        public SeasonUpdateDTOValidator(IStringLocalizer<CreateDTOMessages> localizer)
        {
            RuleFor(season => season.Name)
                .NotEmpty()
                .WithMessage(localizer.GetString("name"));
            
            RuleFor(season => season.SeriesId)
                .NotEmpty()
                .WithMessage(localizer.GetString("series"));

            RuleFor(season => season.StartDate)
                .NotEmpty()
                .WithMessage(localizer.GetString("startdate"));

            RuleFor(season => season.EndDate)
                .NotEmpty()
                .WithMessage(localizer.GetString("enddate"));
        }
    }
}