using AutoMapper;
using Racing.DAL.Models;
using Racing.DTO.CreateDTO;
using Racing.DTO.ReadDTO;
using Racing.DTO.UpdateDTO;

namespace Racing.BLL
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateReverseMap<Circuit, CircuitDTO, CircuitCreateDTO, CircuitUpdateDTO>();
            CreateReverseMap<Country, CountryDTO, CountryCreateDTO, CountryUpdateDTO>();
            CreateReverseMap<Pilot, PilotDTO, PilotCreateDTO, PilotUpdateDTO>();
            CreateReverseMap<Race, RaceDTO, RaceCreateDTO, RaceUpdateDTO>();
            CreateReverseMap<Season, SeasonDTO, SeasonCreateDTO, SeasonUpdateDTO>();
            CreateReverseMap<Series, SeriesDTO, SeriesCreateDTO, SeriesUpdateDTO>();
            CreateReverseMap<TeamParticipant, TeamParticipantsDTO, TeamParticipantsCreateDTO,
                TeamParticipantsUpdateDTO>();
            CreateReverseMap<Team, TeamDTO, TeamCreateDTO, TeamUpdateDTO>();
        }

        private void CreateReverseMap<TEntity, TDTO, TCreateDTO, TUpdateDTO>()
        {
            CreateMap<TEntity, TDTO>().ReverseMap();
            CreateMap<TEntity, TCreateDTO>().ReverseMap();
            CreateMap<TEntity, TUpdateDTO>().ReverseMap();
        }
    }
}