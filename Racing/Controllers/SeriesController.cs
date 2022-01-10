using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Racing.BLL.Services.Interface;
using Racing.DTO.CreateDTO;
using Racing.DTO.UpdateDTO;

namespace Racing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesService _service;

        public SeriesController(ISeriesService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeries([FromBody] SeriesCreateDTO seriesCreateDto)
        {
            var toCreate = await _service.Create(seriesCreateDto);

            return Created("", toCreate);
        }

        [HttpGet]
        public async Task<IActionResult> ReadSeries([FromQuery(Name = "items_per_page")] int? itemsPerPage,
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
        public async Task<IActionResult> UpdateSeries([FromBody] SeriesUpdateDTO seriesUpdateDto)
        {
            var toUpdate = await _service.Update(seriesUpdateDto);
            return Ok(toUpdate);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSeries(int id)
        {
            var toDelete = await _service.Delete(id);
            return Ok(toDelete);
        }
    }
}