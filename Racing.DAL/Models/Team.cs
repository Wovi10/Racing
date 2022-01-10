using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Racing.DAL.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TeamParticipant> TeamParticipants { get; set; }
    }
}