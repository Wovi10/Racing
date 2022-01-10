using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Racing.BLL.Services.Interface;
using Racing.DAL;
using Racing.DAL.Context;
using Racing.DAL.Models;
using Racing.DAL.Repositories.Interface;
using Racing.DTO;
using Racing.DTO.CreateDTO;
using Racing.DTO.ReadDTO;
using Racing.DTO.UpdateDTO;

namespace Racing.BLL.Services
{
    public class SeriesService : ISeriesService
    {
        private IUnitOfWork _uow;
        private ISeriesRepository _repository;
        private readonly IMapper _mapper;

        public SeriesService(IUnitOfWork uow, ISeriesRepository repository, IMapper mapper)
        {
            _uow = uow;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SeriesDTO> Create(SeriesCreateDTO entity)
        {
            var mappedSeries = _mapper.Map<Series>(entity);
            var entityEntry = await _repository.Insert(mappedSeries);
            _uow.Save();

            return _mapper.Map<SeriesDTO>(entityEntry.Entity);
        }

        public async Task<SeriesDTO> GetOne(int id)
        {
            var series = await _repository.GetOne(id);
            return _mapper.Map<SeriesDTO>(series);
        }

        public async Task<List<SeriesDTO>> ReadPage(int page, int itemsPerPage)
        {
            return await Task.Run(() =>
            {
                var list = _repository.Get();
                return _mapper.Map<List<SeriesDTO>>(list.Result.Skip(page * itemsPerPage).ToList());
            });
        }

        public async Task<SeriesDTO> Update(SeriesUpdateDTO entity)
        {
            var series = await _repository.Update(_mapper.Map<Series>(entity));
            _uow.Save();
            return _mapper.Map<SeriesDTO>(series);
        }

        public async Task<SeriesDTO> Delete(int id)
        {
            var entity = await GetOne(id);
            var series = _mapper.Map<Series>(entity);
            series = await _repository.Delete(series);
            _uow.Save();
            return _mapper.Map<SeriesDTO>(series);
        }
    }
}