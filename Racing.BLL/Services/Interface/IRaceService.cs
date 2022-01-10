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
    public interface IRaceService
    {
        public Task<RaceDTO> Create(RaceCreateDTO entity);
        public Task<RaceDTO> GetOne(int id);
        public Task<List<RaceDTO>> ReadPage(int page, int itemsPerPage);
        public Task<RaceDTO> Update(RaceUpdateDTO entity);
        public Task<RaceDTO> Delete(int id);
    }
}