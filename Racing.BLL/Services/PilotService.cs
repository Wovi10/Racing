using System;
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
    public class PilotService : IPilotService
    {
        private IUnitOfWork _uow;
        private IPilotRepository _repository;
        private readonly IMapper _mapper;

        public PilotService(IUnitOfWork uow, IPilotRepository repository, IMapper mapper)
        {
            _uow = uow;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PilotDTO> Create(PilotCreateDTO entity)
        {
            var mappedPilot = _mapper.Map<Pilot>(entity);
            var entityEntry = await _repository.Insert(mappedPilot);
            _uow.Save();

            return _mapper.Map<PilotDTO>(entityEntry.Entity);
        }

        public async Task<PilotDTO> GetOne(int id)
        {
            var pilot = await _repository.GetOne(id);
            return _mapper.Map<PilotDTO>(pilot);
        }

        public async Task<List<PilotDTO>> ReadPage(int page, int itemsPerPage)
        {
            return await Task.Run(() =>
            {
                var list = _repository.Get();
                return _mapper.Map<List<PilotDTO>>(list.Result.Skip(page * itemsPerPage).ToList());
            });
        }

        public async Task<PilotDTO> Update(PilotUpdateDTO entity)
        {
            var pilot = await _repository.Update(_mapper.Map<Pilot>(entity));
            _uow.Save();
            return _mapper.Map<PilotDTO>(pilot);
        }

        public Task<PilotDTO> Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<PilotDTO> Delete(int id)
        {
            var entity = await GetOne(id);
            var pilot = _mapper.Map<Pilot>(entity);
            pilot = await _repository.Delete(pilot);
            _uow.Save();
            return _mapper.Map<PilotDTO>(pilot);
        }
    }
}