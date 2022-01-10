using System;

namespace Racing.DTO.ReadDTO
{
    public class RaceDTO
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public int CircuitId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}