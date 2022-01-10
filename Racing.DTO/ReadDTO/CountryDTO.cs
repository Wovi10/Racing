namespace Racing.DTO.ReadDTO
{
    public class CountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CountryDTO()
        {
        }

        public CountryDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}";
        }
    }
}