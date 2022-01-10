using System;

namespace Racing.DTO.CreateDTO
{
    public class RaceCreateDTO
    {
        public int SeasonId { get; set; }
        public int CircuitId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}