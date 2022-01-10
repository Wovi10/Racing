using System;
using Racing.DTO.CreateDTO;

namespace Racing.DTO.UpdateDTO
{
    public class SeasonUpdateDTO
    {
        public int Id { get; set; }
        public int SeriesId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }
    }
}