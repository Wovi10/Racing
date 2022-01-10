using Racing.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Racing.DTO.CreateDTO;
using Racing.DTO.ReadDTO;
using Racing.DTO.UpdateDTO;

namespace Racing.BLL.Services.Interface
{
    public interface ITeamParticipantService
    {
        public Task<TeamParticipantsDTO> Create(TeamParticipantsCreateDTO entity);
        public Task<TeamParticipantsDTO> GetOne(int id);
        public Task<List<TeamParticipantsDTO>> ReadPage(int page, int itemsPerPage);
        public Task<TeamParticipantsDTO> Update(TeamParticipantsUpdateDTO entity);
        public Task<TeamParticipantsDTO> Delete(int id);
    }
}