using Racing.DAL.Context;
using Racing.DAL.Models;
using Racing.DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Racing.DAL.Repositories
{
    public class TeamParticipantRepository : GenericRepository<TeamParticipant>, ITeamParticipantRepository
    {
        public TeamParticipantRepository(DBRacingContext context) : base(context)
        {
        }
    }
}