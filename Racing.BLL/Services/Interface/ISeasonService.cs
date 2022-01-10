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
    public interface ISeasonService
    {
        public Task<SeasonDTO> Create(SeasonCreateDTO entity);
        public Task<SeasonDTO> GetOne(int id);
        public Task<List<SeasonDTO>> ReadPage(int page, int itemsPerPage);
        public Task<SeasonDTO> Update(SeasonUpdateDTO entity);
        public Task<SeasonDTO> Delete(int id);
    }
}