using FluentValidation;
using Microsoft.Extensions.Localization;
using Racing.DTO.Resources;

namespace Racing.DTO.UpdateDTO.FluentValidation
{
    public class TeamParticipantUpdateDTOValidator : AbstractValidator<TeamParticipantsUpdateDTO>
    {
        public TeamParticipantUpdateDTOValidator(IStringLocalizer<CreateDTOMessages> localizer)
        {
            RuleFor(tp => tp.PilotId)
                .NotEmpty()
                .WithMessage(localizer.GetString("pilot"));
            
            RuleFor(tp => tp.RaceId)
                .NotEmpty()
                .WithMessage(localizer.GetString("race"));
            
            RuleFor(tp => tp.TeamId)
                .NotEmpty()
                .WithMessage(localizer.GetString("team"));
        }
    }
}