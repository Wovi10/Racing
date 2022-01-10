using Racing.DAL.Context;
using Racing.DAL.Models;
using Racing.DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Racing.DAL.Repositories
{
    public class CircuitRepository : GenericRepository<Circuit>, ICircuitRepository
    {
        public CircuitRepository(DBRacingContext context) : base(context)
        {
        }
    }
}