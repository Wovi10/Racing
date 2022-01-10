using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Racing.BLL.Services.Interface;
using Racing.DAL.Models;
using Racing.DTO.CreateDTO;
using Racing.DTO.ReadDTO;
using Racing.DTO.UpdateDTO;

namespace Racing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamParticipantController : ControllerBase
    {
        private readonly ITeamParticipantService _service;

        public TeamParticipantController(ITeamParticipantService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeamParticipant(
            [FromBody] TeamParticipantsCreateDTO teamParticipantsCreateDto)
        {
            var toCreate = await _service.Create(teamParticipantsCreateDto);

            return Created("", toCreate);
        }

        [HttpGet]
        public async Task<IActionResult> ReadTeamParticipant([FromQuery(Name = "items_per_page")] int? itemsPerPage,
            [FromQuery(Name = "page")] int? page, [FromQuery(Name = "id")] int? id)
        {
            if (itemsPerPage != null && page != null && id != null)
            {
                var toRead = await _service.ReadPage((int) page, (int) itemsPerPage);
                return Ok(toRead);
            }
            else if (itemsPerPage == null && page == null && id != null)
            {
                var toRead = await _service.GetOne((int) id);
                return Ok(toRead);
            }
            else
            {
                var toRead = await _service.ReadPage(0, 40);
                return Ok(toRead);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeamParticipant(
            [FromBody] TeamParticipantsUpdateDTO teamParticipantsUpdateDto)
        {
            var toUpdate = await _service.Update(teamParticipantsUpdateDto);
            return Ok(toUpdate);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTeamParticipant(int id)
        {
            var toDelete = await _service.Delete(id);
            return Ok(toDelete);
        }
    }
}