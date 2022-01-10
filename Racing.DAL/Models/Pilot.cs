using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Racing.DAL.Models
{
    public class Pilot
    {
        public int Id { get; set; }
        public string LicensNr { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string NickName { get; set; }
        public string PhotoPath { get; set; }
        public string Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public int Length { get; set; }
        public decimal Weight { get; set; }

        public List<TeamParticipant> TeamParticipants { get; set; }
    }
}