using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Racing.DAL.Models
{
    public class TeamParticipant
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int RaceId { get; set; }
        public int PilotId { get; set; }
    }
}