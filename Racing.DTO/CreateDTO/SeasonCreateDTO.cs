using System;

namespace Racing.DTO.CreateDTO
{
    public class SeasonCreateDTO
    {
        public int SeriesId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }
    }
}