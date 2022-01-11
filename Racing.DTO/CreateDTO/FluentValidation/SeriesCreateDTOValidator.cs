using FluentValidation;
using Microsoft.Extensions.Localization;
using Racing.DTO.Resources;

namespace Racing.DTO.CreateDTO.FluentValidation
{
    public class SeriesCreateDTOValidator : AbstractValidator<SeriesCreateDTO>
    {
        public SeriesCreateDTOValidator(IStringLocalizer<CreateDTOMessages> localizer)
        {
            RuleFor(series => series.Name)
                .NotEmpty()
                .WithMessage(localizer.GetString("name"));
            
            RuleFor(series => series.SortingOrder)
                .NotEmpty()
                .WithMessage(localizer.GetString("sortingorder"));

            RuleFor(series => series.StartDate)
                .NotEmpty()
                .WithMessage(localizer.GetString("startdate"));

            RuleFor(series => series.StartDate)
                .LessThanOrEqualTo(series => series.EndDate)
                .WithMessage(localizer.GetString("wrongdate"));
            
            RuleFor(series => series.EndDate)
                .NotEmpty()
                .WithMessage(localizer.GetString("enddate"));
        }
    }
}