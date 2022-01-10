using System;

namespace Racing.DTO.ReadDTO
{
    public class PilotDTO
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
    }
}