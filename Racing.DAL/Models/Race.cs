using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Racing.DAL.Models
{
    public class Race
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public int CircuitId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<TeamParticipant> TeamParticipants { get; set; }
    }
}