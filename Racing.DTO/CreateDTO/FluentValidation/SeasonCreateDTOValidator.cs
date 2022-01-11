using FluentValidation;
using Microsoft.Extensions.Localization;
using Racing.DTO.Resources;

namespace Racing.DTO.CreateDTO.FluentValidation
{
    public class SeasonCreateDTOValidator : AbstractValidator<SeasonCreateDTO>
    {
        public SeasonCreateDTOValidator(IStringLocalizer<CreateDTOMessages> localizer)
        {
            RuleFor(season => season.Name)
                .NotEmpty()
                .WithMessage(localizer.GetString("name"));
            
            RuleFor(season => season.SeriesId)
                .NotEmpty()
                .WithMessage(localizer.GetString("series"));

            RuleFor(season => season.StartDate)
                .NotEmpty()
                .LessThanOrEqualTo(season => season.EndDate)
                .WithMessage(localizer.GetString("startdate"));
            
            RuleFor(season => season.StartDate)
                .LessThanOrEqualTo(season => season.EndDate)
                .WithMessage(localizer.GetString("wrongdate"));

            RuleFor(season => season.EndDate)
                .NotEmpty()
                .WithMessage(localizer.GetString("enddate"));
        }
    }
}