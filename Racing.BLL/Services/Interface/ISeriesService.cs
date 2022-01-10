using Racing.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Racing.DTO;
using Racing.DTO.CreateDTO;
using Racing.DTO.ReadDTO;
using Racing.DTO.UpdateDTO;

namespace Racing.BLL.Services.Interface
{
    public interface ISeriesService
    {
        public Task<SeriesDTO> Create(SeriesCreateDTO entity);
        public Task<SeriesDTO> GetOne(int id);
        public Task<List<SeriesDTO>> ReadPage(int page, int itemsPerPage);
        public Task<SeriesDTO> Update(SeriesUpdateDTO entity);
        public Task<SeriesDTO> Delete(int id);
    }
}