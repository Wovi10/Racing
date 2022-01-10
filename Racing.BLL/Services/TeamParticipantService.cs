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
    public class TeamParticipantService : ITeamParticipantService
    {
        private IUnitOfWork _uow;
        private ITeamParticipantRepository _repository;
        private readonly IMapper _mapper;

        public TeamParticipantService(IUnitOfWork uow, ITeamParticipantRepository repository, IMapper mapper)
        {
            _uow = uow;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TeamParticipantsDTO> Create(TeamParticipantsCreateDTO entity)
        {
            var mappedTP = _mapper.Map<TeamParticipant>(entity);
            var entityEntry = await _repository.Insert(mappedTP);
            _uow.Save();

            return _mapper.Map<TeamParticipantsDTO>(entityEntry.Entity);
        }

        public async Task<TeamParticipantsDTO> GetOne(int id)
        {
            var tP = await _repository.GetOne(id);
            return _mapper.Map<TeamParticipantsDTO>(tP);
        }

        public async Task<List<TeamParticipantsDTO>> ReadPage(int page, int itemsPerPage)
        {
            return await Task.Run(() =>
            {
                var list = _repository.Get();
                return _mapper.Map<List<TeamParticipantsDTO>>(list.Result.Skip(page * itemsPerPage).ToList());
            });
        }

        public async Task<TeamParticipantsDTO> Update(TeamParticipantsUpdateDTO entity)
        {
            var tP = await _repository.Update(_mapper.Map<TeamParticipant>(entity));
            _uow.Save();
            return _mapper.Map<TeamParticipantsDTO>(tP);
        }

        public async Task<TeamParticipantsDTO> Delete(int id)
        {
            var entity = await GetOne(id);
            var tP = _mapper.Map<TeamParticipant>(entity);
            tP = await _repository.Delete(tP);
            _uow.Save();
            return _mapper.Map<TeamParticipantsDTO>(tP);
        }
    }
}