using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Racing.DTO.ReadDTO;

namespace Racing.UI.Pages.Circuit
{
    public class IndexModel : PageModel
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public string responseString;
        public List<CircuitDTO> circuits = new List<CircuitDTO>();
        private static string basicURL = "https://localhost:44397/api/";
        private string circuitURL = basicURL + "Circuit";
        private string countryURL = basicURL + "Country";

        public IndexModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        public async Task<List<CircuitDTO>> GetCircuits()
        {
            HttpResponseMessage response = await _client.GetAsync(circuitURL + "?items_per_page=50&page=0");
            responseString = await response.Content.ReadAsStringAsync();
            var circuitList = JsonSerializer.Deserialize<List<CircuitDTO>>(responseString, _options);

            foreach (var circuit in circuitList)
            {
                CircuitDTO addCircuit = new CircuitDTO()
                {
                    Id = circuit.Id,
                    Name = circuit.Name,
                    Length = circuit.Length,
                    CountryId = circuit.CountryId,
                    State = circuit.State,
                    Street = circuit.Street,
                    Number = circuit.Number
                };
                circuits.Add(addCircuit);
            }

            return circuits;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            circuits = await GetCircuits();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            int id = int.Parse(Request.Form["Id"]);

            HttpResponseMessage response = await _client.GetAsync(circuitURL + "?id=" + id);
            string getResponse = await response.Content.ReadAsStringAsync();
            CircuitDTO deleteCircuit = JsonSerializer.Deserialize<CircuitDTO>(getResponse, _options);

            if (deleteCircuit.Id == id)
            {
                await _client.DeleteAsync(circuitURL + "/" + id);
            }

            responseString = getResponse;
            circuits = await GetCircuits();
            return Page();
        }

        public async Task<string> GetCountryName(int itemCountryId)
        {
            HttpResponseMessage response = await _client.GetAsync(countryURL + "?id=" + itemCountryId);
            responseString = await response.Content.ReadAsStringAsync();
            CountryDTO country = JsonSerializer.Deserialize<CountryDTO>(responseString, _options);
            string countryName = country.Name;

            return countryName;
        }
    }
}