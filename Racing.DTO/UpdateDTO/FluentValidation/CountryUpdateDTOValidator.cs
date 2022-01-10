using FluentValidation;
using Microsoft.Extensions.Localization;
using Racing.DTO.Resources;

namespace Racing.DTO.UpdateDTO.FluentValidation
{
    public class CountryUpdateDTOValidator : AbstractValidator<CountryUpdateDTO>
    {
        public CountryUpdateDTOValidator(IStringLocalizer<CreateDTOMessages> localizer)
        {
            RuleFor(country => country.Name)
                .NotEmpty()
                .WithMessage(localizer.GetString("name"));
        }
    }
}