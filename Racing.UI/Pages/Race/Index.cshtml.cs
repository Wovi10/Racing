using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Racing.DTO.ReadDTO;

namespace Racing.UI.Pages.Race
{
    public class IndexModel : PageModel
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public string responseString;
        public List<RaceDTO> races = new List<RaceDTO>();
        private static string basicURL = "https://localhost:44397/api/";
        private string raceURL = basicURL + "Race";
        private string seasonURL = basicURL + "Season";
        private string circuitURL = basicURL + "Circuit";

        public IndexModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        public async Task<List<RaceDTO>> GetRaces()
        {
            HttpResponseMessage response = await _client.GetAsync(raceURL + "?items_per_page=50&page=0");
            responseString = await response.Content.ReadAsStringAsync();
            var raceList = JsonSerializer.Deserialize<List<RaceDTO>>(responseString, _options);

            foreach (var race in raceList)
            {
                RaceDTO addRace = new RaceDTO()
                {
                    Id = race.Id,
                    Name = race.Name,
                    StartDate = race.StartDate,
                    EndDate = race.EndDate,
                    SeasonId = race.SeasonId,
                    CircuitId = race.CircuitId
                };
                races.Add(addRace);
            }

            return races;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            races = await GetRaces();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            int id = int.Parse(Request.Form["Id"]);

            HttpResponseMessage response = await _client.GetAsync(raceURL + "?id=" + id);
            string getResponse = await response.Content.ReadAsStringAsync();
            RaceDTO deleteRace = JsonSerializer.Deserialize<RaceDTO>(getResponse, _options);

            if (deleteRace.Id == id)
            {
                await _client.DeleteAsync(raceURL + "/" + id);
            }

            responseString = getResponse;
            races = await GetRaces();
            return Page();
        }

        public async Task<string> GetSeasonName(int itemSeasonId)
        {
            HttpResponseMessage response = await _client.GetAsync(seasonURL + "?id=" + itemSeasonId);
            responseString = await response.Content.ReadAsStringAsync();
            SeasonDTO season = JsonSerializer.Deserialize<SeasonDTO>(responseString, _options);
            string seasonName = season.Name;

            return seasonName;
        }

        public async Task<string> GetCircuitName(int itemCircuitId)
        {
            HttpResponseMessage response = await _client.GetAsync(circuitURL + "?id=" + itemCircuitId);
            responseString = await response.Content.ReadAsStringAsync();
            CircuitDTO circuit = JsonSerializer.Deserialize<CircuitDTO>(responseString, _options);
            string circuitName = circuit.Name;

            return circuitName;
        }
    }
}