namespace Racing.DTO.CreateDTO
{
    public class TeamParticipantsCreateDTO
    {
        public int TeamId { get; set; }
        public int RaceId { get; set; }
        public int PilotId { get; set; }
    }
}