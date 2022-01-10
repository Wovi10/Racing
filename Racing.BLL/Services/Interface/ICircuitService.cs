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
    public interface ICircuitService
    {
        public Task<CircuitDTO> Create(CircuitCreateDTO entity);
        public Task<CircuitDTO> GetOne(int id);
        public Task<List<CircuitDTO>> ReadPage(int page, int itemsPerPage);
        public Task<CircuitDTO> Update(CircuitUpdateDTO entity);
        public Task<CircuitDTO> Delete(int id);
    }
}