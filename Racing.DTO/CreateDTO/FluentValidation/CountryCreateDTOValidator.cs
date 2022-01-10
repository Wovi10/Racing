using FluentValidation;
using Microsoft.Extensions.Localization;
using Racing.DTO.Resources;

namespace Racing.DTO.CreateDTO.FluentValidation
{
    public class CountryCreateDTOValidator : AbstractValidator<CountryCreateDTO>
    {
        public CountryCreateDTOValidator(IStringLocalizer<CreateDTOMessages> localizer)
        {
            RuleFor(country => country.Name)
                .NotEmpty()
                .WithMessage(localizer.GetString("name"));
        }
    }
}