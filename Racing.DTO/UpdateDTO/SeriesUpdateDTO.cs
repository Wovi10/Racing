using System;

namespace Racing.DTO.UpdateDTO
{
    public class SeriesUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SortingOrder { get; set; }
    }
}