using FluentValidation;
using Microsoft.Extensions.Localization;
using Racing.DTO.Resources;

namespace Racing.DTO.CreateDTO.FluentValidation
{
    public class TeamParticipantCreateDTOValidator : AbstractValidator<TeamParticipantsCreateDTO>
    {
        public TeamParticipantCreateDTOValidator(IStringLocalizer<CreateDTOMessages> localizer)
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