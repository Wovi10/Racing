using Racing.BLL.Services.Interface;
using Racing.DAL.Context;
using Racing.DAL.Models;
using Racing.DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Racing.DAL;
using Racing.DTO.CreateDTO;
using Racing.DTO.ReadDTO;
using Racing.DTO.UpdateDTO;

namespace Racing.BLL.Services
{
    public class RaceService : IRaceService
    {
        private IUnitOfWork _uow;
        private IRaceRepository _repository;
        private readonly IMapper _mapper;

        public RaceService(IUnitOfWork uow, IRaceRepository repository, IMapper mapper)
        {
            _uow = uow;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RaceDTO> Create(RaceCreateDTO entity)
        {
            var mappedRace = _mapper.Map<Race>(entity);
            var entityEntry = await _repository.Insert(mappedRace);
            _uow.Save();

            return _mapper.Map<RaceDTO>(entityEntry.Entity);
        }

        public async Task<RaceDTO> GetOne(int id)
        {
            var race = await _repository.GetOne(id);
            return _mapper.Map<RaceDTO>(race);
        }

        public async Task<List<RaceDTO>> ReadPage(int page, int itemsPerPage)
        {
            return await Task.Run(() =>
            {
                var list = _repository.Get();
                return _mapper.Map<List<RaceDTO>>(list.Result.Skip(page * itemsPerPage).ToList());
            });
        }

        public async Task<RaceDTO> Update(RaceUpdateDTO entity)
        {
            var race = await _repository.Update(_mapper.Map<Race>(entity));
            _uow.Save();
            return _mapper.Map<RaceDTO>(race);
        }

        public async Task<RaceDTO> Delete(int id)
        {
            var entity = await GetOne(id);
            var race = _mapper.Map<Race>(entity);
            race = await _repository.Delete(race);
            _uow.Save();
            return _mapper.Map<RaceDTO>(race);
        }
    }
}