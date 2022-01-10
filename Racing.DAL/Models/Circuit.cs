using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Racing.DAL.Models
{
    public class Circuit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Length { get; set; }

        public int CountryId { get; set; }

        //public Country Country { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public List<Race> Races { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Length)}: {Length}, " +
                $"{nameof(CountryId)}: {CountryId}, {nameof(State)}: {State}, {nameof(Street)}: {Street}, " +
                $"{nameof(Number)}: {Number}";
        }
    }
}