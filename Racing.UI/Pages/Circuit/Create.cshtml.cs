using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Racing.DAL.Repositories;
using Racing.DTO;
using Racing.DTO.CreateDTO;
using Racing.DTO.ReadDTO;
using Racing.DTO.UpdateDTO;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Racing.UI.Pages.Circuit
{
    public class CreateModel : PageModel
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _options;
        
        private static string basicURL = "https://localhost:44397/api/";
        private string circuitURL = basicURL + "Circuit";
        private string countryURL = basicURL + "Country";
        
        public string responseString;
        
        public List<CountryDTO> Countries = new List<CountryDTO>();

        public CreateModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        public string Name { get; set; }
        public decimal Length { get; set; }
        public int CountryId { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public SelectList CountryList { get; set; }
        
        [BindProperty]
        public CircuitCreateDTO circuit { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            Countries = await GetCountries();
            CountryList = new SelectList(Countries, nameof(DAL.Models.Country.Id), nameof(DAL.Models.Country.Name));

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
            if (! ModelState.IsValid)
            {
                return Page();
            }
            
            // CircuitCreateDTO circuitCreateDTO = new CircuitCreateDTO()
            // {
            //     Name = Request.Form[nameof(Name)],
            //     Length = decimal.Parse(Request.Form[nameof(Length)]),
            //     CountryId = int.Parse(Request.Form[nameof(CountryId)]),
            //     State = Request.Form[nameof(State)],
            //     Street = Request.Form[nameof(Street)],
            //     Number = int.Parse(Request.Form[nameof(Number)])
            // };
            
            string jsonString = JsonSerializer.Serialize(circuit);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await _client.PostAsync(circuitURL, stringContent);

            return Redirect("../circuit");
        }
    }
}