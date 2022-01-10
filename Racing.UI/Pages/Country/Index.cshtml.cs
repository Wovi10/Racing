using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Racing.DTO.ReadDTO;

namespace Racing.UI.Pages.Country
{
    public class IndexModel : PageModel
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public string responseString;
        public List<CountryDTO> countries = new List<CountryDTO>();
        private static string basicURL = "https://localhost:44397/api/";
        private string countryURL = basicURL +  "Country";

        public IndexModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        public async Task<List<CountryDTO>> GetCountries()
        {
            HttpResponseMessage response = await _client.GetAsync(countryURL + "?items_per_page=50&page=0");
            responseString = await response.Content.ReadAsStringAsync();
            var countryList = JsonSerializer.Deserialize<List<CountryDTO>>(responseString, _options);

            foreach (var country in countryList)
            {
                CountryDTO addCountry = new CountryDTO()
                {
                    Id = country.Id,
                    Name = country.Name
                };
                countries.Add(addCountry);
            }

            return countries;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            countries = await GetCountries();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            int id = int.Parse(Request.Form["Id"]);

            HttpResponseMessage response = await _client.GetAsync(countryURL + "?id=" + id);
            string getResponse = await response.Content.ReadAsStringAsync();
            CountryDTO deleteCountry = JsonSerializer.Deserialize<CountryDTO>(getResponse, _options);

            if (deleteCountry.Id == id)
            {
                await _client.DeleteAsync(countryURL + "/" + id);
            }

            responseString = getResponse;
            countries = await GetCountries();
            return Page();
        }
    }
}