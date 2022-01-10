using System;
using System.Collections.Generic;

namespace Racing.DAL.Models
{
    public class Series
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SortingOrder { get; set; }

        public List<Season> Seasons { get; set; }
    }
}