using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Racing.BLL.Services.Interface;
using Racing.DTO.CreateDTO;
using Racing.DTO.UpdateDTO;

namespace Racing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _service;

        public TeamController(ITeamService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] TeamCreateDTO teamCreateDto)
        {
            var toCreate = await _service.Create(teamCreateDto);

            return Created("", toCreate);
        }

        [HttpGet]
        public async Task<IActionResult> ReadTeam([FromQuery(Name = "items_per_page")] int? itemsPerPage,
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
        public async Task<IActionResult> UpdateTeam([FromBody] TeamUpdateDTO teamUpdateDto)
        {
            var toUpdate = await _service.Update(teamUpdateDto);
            return Ok(toUpdate);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var toDelete = await _service.Delete(id);
            return Ok(toDelete);
        }
    }
}