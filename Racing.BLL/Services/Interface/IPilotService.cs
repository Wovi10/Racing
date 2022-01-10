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
    public interface IPilotService
    {
        public Task<PilotDTO> Create(PilotCreateDTO entity);
        public Task<PilotDTO> GetOne(int id);
        public Task<List<PilotDTO>> ReadPage(int page, int itemsPerPage);
        public Task<PilotDTO> Update(PilotUpdateDTO entity);
        public Task<PilotDTO> Delete(int id);
    }
}