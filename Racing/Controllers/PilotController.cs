using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Racing.BLL.Services.Interface;
using Racing.DAL.Models;
using Racing.DTO.CreateDTO;
using Racing.DTO.UpdateDTO;

namespace Racing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PilotController : ControllerBase
    {
        private readonly IPilotService _service;

        public PilotController(IPilotService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePilot([FromBody] PilotCreateDTO pilotCreateDto)
        {
            var toCreate = await _service.Create(pilotCreateDto);

            return Created("", toCreate);
        }

        [HttpGet]
        public async Task<IActionResult> ReadPilot([FromQuery(Name = "items_per_page")] int? itemsPerPage,
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
                var pilotList = await _service.ReadPage(0, 40);
                return Ok(pilotList);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePilot([FromBody] PilotUpdateDTO pilotUpdateDto)
        {
            var toUpdate = await _service.Update(pilotUpdateDto);
            return Ok(toUpdate);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePilot(int id)
        {
            var toDelete = await _service.Delete(id);
            return Ok(toDelete);
        }
    }
}