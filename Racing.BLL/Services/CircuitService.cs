using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Racing.BLL.Services.Interface;
using Racing.DAL;
using Racing.DAL.Models;
using Racing.DAL.Repositories.Interface;
using Racing.DTO.CreateDTO;
using Racing.DTO.ReadDTO;
using Racing.DTO.UpdateDTO;

namespace Racing.BLL.Services
{
    public class CircuitService : ICircuitService
    {
        private IUnitOfWork _uow;
        private ICircuitRepository _repository;
        private readonly IMapper _mapper;

        public CircuitService(IUnitOfWork uow, ICircuitRepository repo, IMapper mapper)
        {
            _uow = uow;
            _repository = repo;
            _mapper = mapper;
        }

        public async Task<CircuitDTO> Create(CircuitCreateDTO entity)
        {
            var mappedCircuit = _mapper.Map<Circuit>(entity);
            var entityEntry = await _repository.Insert(mappedCircuit);
            _uow.Save();

            return _mapper.Map<CircuitDTO>(entityEntry.Entity);
        }

        public async Task<CircuitDTO> GetOne(int id)
        {
            var circuit = await _repository.GetOne(id);
            return _mapper.Map<CircuitDTO>(circuit);
        }

        public async Task<List<CircuitDTO>> ReadPage(int page, int itemsPerPage)
        {
            return await Task.Run(() =>
            {
                var list = _repository.Get();
                return _mapper.Map<List<CircuitDTO>>(list.Result.Skip(page * itemsPerPage).ToList());
            });
        }

        public async Task<CircuitDTO> Update(CircuitUpdateDTO entity)
        {
            var circuit = await _repository.Update(_mapper.Map<Circuit>(entity));
            _uow.Save();
            return _mapper.Map<CircuitDTO>(circuit);
        }

        public async Task<CircuitDTO> Delete(int id)
        {
            var entity = await GetOne(id);
            var circuit = _mapper.Map<Circuit>(entity);
            circuit = await _repository.Delete(circuit);
            _uow.Save();
            return _mapper.Map<CircuitDTO>(circuit);
        }
    }
}