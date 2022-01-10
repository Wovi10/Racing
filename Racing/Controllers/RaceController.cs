using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Racing.BLL.Services.Interface;
using Racing.DTO.CreateDTO;
using Racing.DTO.UpdateDTO;

namespace Racing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private readonly IRaceService _service;

        public RaceController(IRaceService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRace([FromBody] RaceCreateDTO raceCreateDto)
        {
            var toCreate = await _service.Create(raceCreateDto);

            return Created("", toCreate);
        }

        [HttpGet]
        public async Task<IActionResult> ReadRace([FromQuery(Name = "items_per_page")] int? itemsPerPage,
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
        public async Task<IActionResult> UpdateRace([FromBody] RaceUpdateDTO raceUpdateDto)
        {
            var toUpdate = await _service.Update(raceUpdateDto);
            return Ok(toUpdate);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRace(int id)
        {
            var toDelete = await _service.Delete(id);
            return Ok(toDelete);
        }
    }
}