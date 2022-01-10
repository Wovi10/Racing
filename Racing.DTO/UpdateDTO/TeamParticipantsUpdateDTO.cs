using Racing.DTO.CreateDTO;

namespace Racing.DTO.UpdateDTO
{
    public class TeamParticipantsUpdateDTO
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int RaceId { get; set; }
        public int PilotId { get; set; }
    }
}