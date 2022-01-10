using System;
using System.Collections.Generic;

namespace Racing.DAL.Models
{
    public class Season
    {
        public int Id { get; set; }

        public int SeriesId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }

        public List<Race> Races { get; set; }
    }
}