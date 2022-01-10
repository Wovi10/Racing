using Racing.DTO.ReadDTO;

namespace Racing.DTO.CreateDTO
{
    public class CircuitCreateDTO
    {
        public string Name { get; set; }
        public decimal Length { get; set; }
        public int CountryId { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(Name)}: {Name}, {nameof(Length)}: {Length}, {nameof(CountryId)}: {CountryId}, " +
                $"{nameof(State)}: {State}, {nameof(Street)}: {Street}, {nameof(Number)}: {Number}";
        }
    }
}