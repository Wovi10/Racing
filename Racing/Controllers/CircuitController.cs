using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Racing.BLL.Services.Interface;
using Racing.DTO.CreateDTO;
using Racing.DTO.UpdateDTO;

namespace Racing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CircuitController : ControllerBase
    {
        private readonly ICircuitService _service;

        public CircuitController(ICircuitService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCircuit([FromBody] CircuitCreateDTO circuitCreateDTO)
        {
            var toCreate = await _service.Create(circuitCreateDTO);

            return Created("", toCreate);
        }

        [HttpGet]
        public async Task<IActionResult> ReadCircuit([FromQuery(Name = "items_per_page")] int? itemsPerPage,
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
        public async Task<IActionResult> UpdateCircuit([FromBody] CircuitUpdateDTO circuitUpdateDto)
        {
            var toUpdate = await _service.Update(circuitUpdateDto);
            return Ok(toUpdate);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCircuit(int id)
        {
            var toDelete = await _service.Delete(id);
            return Ok(toDelete);
        }
    }
}