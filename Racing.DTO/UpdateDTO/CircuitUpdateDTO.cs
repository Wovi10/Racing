using Racing.DTO.CreateDTO;

namespace Racing.DTO.UpdateDTO
{
    public class CircuitUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Length { get; set; }
        public int CountryId { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
    }
}