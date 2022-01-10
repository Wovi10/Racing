using Racing.DAL.Context;
using Racing.DAL.Models;
using Racing.DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Racing.DAL.Repositories
{
    public class RaceRepository : GenericRepository<Race>, IRaceRepository
    {
        public RaceRepository(DBRacingContext context) : base(context)
        {
        }
    }
}