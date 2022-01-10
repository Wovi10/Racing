using FluentValidation;
using Microsoft.Extensions.Localization;
using Racing.DTO.Resources;

namespace Racing.DTO.CreateDTO.FluentValidation
{
    public class SeriesCreateDTOValidator : AbstractValidator<SeriesCreateDTO>
    {
        public SeriesCreateDTOValidator(IStringLocalizer<CreateDTOMessages> localizer)
        {
            RuleFor(season => season.Name)
                .NotEmpty()
                .WithMessage(localizer.GetString("name"));
            
            RuleFor(season => season.SortingOrder)
                .NotEmpty()
                .WithMessage(localizer.GetString("sortingorder"));

            RuleFor(season => season.StartDate)
                .NotEmpty()
                .WithMessage(localizer.GetString("startdate"));

            RuleFor(season => season.EndDate)
                .NotEmpty()
                .WithMessage(localizer.GetString("enddate"));
        }
    }
}