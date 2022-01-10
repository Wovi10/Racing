using System.Collections.Generic;
using System.Threading.Tasks;
using Racing.DTO.CreateDTO;
using Racing.DTO.ReadDTO;
using Racing.DTO.UpdateDTO;

namespace Racing.BLL.Services.Interface
{
    public interface ICountryService
    {
        public Task<CountryDTO> Create(CountryCreateDTO countryCreateDTO);
        public Task<CountryDTO> GetOne(int id);
        public Task<List<CountryDTO>> ReadPage(int page, int itemsPerPage);
        public Task<CountryDTO> Update(CountryUpdateDTO entity);
        public Task<CountryDTO> Delete(int id);
    }
}