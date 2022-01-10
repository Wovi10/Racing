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
    public interface ITeamService
    {
        public Task<TeamDTO> Create(TeamCreateDTO entity);
        public Task<TeamDTO> GetOne(int id);
        public Task<List<TeamDTO>> ReadPage(int page, int itemsPerPage);
        public Task<TeamDTO> Update(TeamUpdateDTO entity);
        public Task<TeamDTO> Delete(int id);
    }
}