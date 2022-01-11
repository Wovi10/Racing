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
    public class CountryService : ICountryService
    {
        private IUnitOfWork _uow;
        private ICountryRepository _repository;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork uow, ICountryRepository repo, IMapper mapper)
        {
            _uow = uow;
            _repository = repo;
            _mapper = mapper;
        }

        public async Task<CountryDTO> Create(CountryCreateDTO countryCreateDTO)
        {
            var mappedCountry = _mapper.Map<Country>(countryCreateDTO);
            var entityEntry = await _repository.Insert(mappedCountry);
            _uow.Save();

            return _mapper.Map<CountryDTO>(entityEntry.Entity);
        }

        public async Task<CountryDTO> GetOne(int id)
        {
            var country = await _repository.GetOne(id);
            return _mapper.Map<CountryDTO>(country);
        }

        public async Task<List<CountryDTO>> ReadPage(int page, int itemsPerPage)
        {
            return await Task.Run(() =>
            {
                var list = _repository.Get();
                return _mapper.Map<List<CountryDTO>>(list.Result.Skip(page * itemsPerPage).ToList());
            });
        }

        public async Task<CountryDTO> Update(CountryUpdateDTO entity)
        {
            var country = await _repository.Update(_mapper.Map<Country>(entity));
            _uow.Save();
            return _mapper.Map<CountryDTO>(country);
        }

        public async Task<CountryDTO> Delete(int id)
        {
            var entity = await GetOne(id);
            var country = _mapper.Map<Country>(entity);
            country = await _repository.Delete(country);
            _uow.Save();
            return _mapper.Map<CountryDTO>(country);
        }
    }
}