namespace Racing.DTO.ReadDTO
{
    public class TeamParticipantsDTO
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int RaceId { get; set; }
        public int PilotId { get; set; }
    }
}