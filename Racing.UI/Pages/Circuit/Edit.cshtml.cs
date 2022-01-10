using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Racing.DTO.ReadDTO;
using Racing.DTO.UpdateDTO;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Racing.UI.Pages.Circuit
{
    public class EditModel : PageModel
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public string responseString;
        private string basicURL = "https://localhost:44397/api/Circuit";
        private string countryURL = "https://localhost:44397/api/Country";
        public List<CountryDTO> Countries = new List<CountryDTO>();

        public EditModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        // public long Id { get; set; }
        // public string Name { get; set; }
        // public int Length { get; set; }
        // public int CountryId { get; set; }
        // public string State { get; set; }
        //
        // public string Street { get; set; }
        // public int Number { get; set; }
        public SelectList CountryList { get; set; }

        [BindProperty] public CircuitUpdateDTO Circuit { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            Countries = await GetCountries();
            CountryList = new SelectList(Countries, nameof(DAL.Models.Country.Id), nameof(DAL.Models.Country.Name));
            HttpResponseMessage response = await _client.GetAsync(basicURL + "?id=" + id);
            responseString = await response.Content.ReadAsStringAsync();
            Circuit = JsonSerializer.Deserialize<CircuitUpdateDTO>(responseString, _options);

            return Page();
        }

        public async Task<List<CountryDTO>> GetCountries()
        {
            HttpResponseMessage response = await _client.GetAsync(countryURL + "?items_per_page=50&page=0");
            responseString = await response.Content.ReadAsStringAsync();
            var countryList = JsonConvert.DeserializeObject<List<CountryDTO>>(responseString);


            foreach (CountryDTO country in countryList)
            {
                CountryDTO addCountry = new CountryDTO()
                {
                    Id = country.Id,
                    Name = country.Name
                };
                Countries.Add(addCountry);
            }

            return Countries;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // CountryUpdateDTO countryDTO = new CountryUpdateDTO()
            // {
            //     ID = long.Parse(Request.Form[nameof(ID)]),
            //     Name = Request.Form[nameof(Name)]
            // };

            if (!ModelState.IsValid)
            {
                return Page();
            }

            string jsonString = JsonSerializer.Serialize(Circuit);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(basicURL, stringContent);
            string requestRe = await response.Content.ReadAsStringAsync();

            responseString = requestRe;

            return Redirect("../circuit");
        }

        // public async Task<string> GetCountryName(int itemCountryId)
        // {
        //     HttpResponseMessage response = await _client.GetAsync(countryURL + "?id=" + itemCountryId);
        //     responseString = await response.Content.ReadAsStringAsync();
        //     CountryDTO country = JsonSerializer.Deserialize<CountryDTO>(responseString, _options);
        //     string countryName = country.Name;
        //
        //     return countryName;
        // }
    }
}