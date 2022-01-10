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
    public class SeasonService : ISeasonService
    {
        private IUnitOfWork _uow;
        private ISeasonRepository _repository;
        private readonly IMapper _mapper;

        public SeasonService(IUnitOfWork uow, ISeasonRepository repository, IMapper mapper)
        {
            _uow = uow;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SeasonDTO> Create(SeasonCreateDTO entity)
        {
            var mappedSeason = _mapper.Map<Season>(entity);
            var entityEntry = await _repository.Insert(mappedSeason);
            _uow.Save();

            return _mapper.Map<SeasonDTO>(entityEntry.Entity);
        }

        public async Task<SeasonDTO> GetOne(int id)
        {
            var season = await _repository.GetOne(id);
            return _mapper.Map<SeasonDTO>(season);
        }

        public async Task<List<SeasonDTO>> ReadPage(int page, int itemsPerPage)
        {
            return await Task.Run(() =>
            {
                var list = _repository.Get();
                return _mapper.Map<List<SeasonDTO>>(list.Result.Skip(page * itemsPerPage).ToList());
            });
        }

        public async Task<SeasonDTO> Update(SeasonUpdateDTO entity)
        {
            var season = await _repository.Update(_mapper.Map<Season>(entity));
            _uow.Save();
            return _mapper.Map<SeasonDTO>(season);
        }

        public async Task<SeasonDTO> Delete(int id)
        {
            var entity = await GetOne(id);
            var season = _mapper.Map<Season>(entity);
            season = await _repository.Delete(season);
            _uow.Save();
            return _mapper.Map<SeasonDTO>(season);
        }
    }
}