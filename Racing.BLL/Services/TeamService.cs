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
    public class TeamService : ITeamService
    {
        private IUnitOfWork _uow;
        private ITeamRepository _repository;
        private readonly IMapper _mapper;

        public TeamService(IUnitOfWork uow, ITeamRepository repository, IMapper mapper)
        {
            _uow = uow;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TeamDTO> Create(TeamCreateDTO entity)
        {
            var mappedTeam = _mapper.Map<Team>(entity);
            var entityEntry = await _repository.Insert(mappedTeam);
            _uow.Save();

            return _mapper.Map<TeamDTO>(entityEntry.Entity);
        }

        public async Task<TeamDTO> GetOne(int id)
        {
            var team = await _repository.GetOne(id);
            return _mapper.Map<TeamDTO>(team);
        }

        public async Task<List<TeamDTO>> ReadPage(int page, int itemsPerPage)
        {
            return await Task.Run(() =>
            {
                var list = _repository.Get();
                return _mapper.Map<List<TeamDTO>>(list.Result.Skip(page * itemsPerPage).ToList());
            });
        }

        public async Task<TeamDTO> Update(TeamUpdateDTO entity)
        {
            var season = await _repository.Update(_mapper.Map<Team>(entity));
            _uow.Save();
            return _mapper.Map<TeamDTO>(season);
        }

        public async Task<TeamDTO> Delete(int id)
        {
            var entity = await GetOne(id);
            var team = _mapper.Map<Team>(entity);
            team = await _repository.Delete(team);
            _uow.Save();
            return _mapper.Map<TeamDTO>(team);
        }
    }
}