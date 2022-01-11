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

namespace Racing.UI.Pages.Race
{
    public class EditModel : PageModel
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public string responseString;
        private static string basicURL = "https://localhost:44397/api/";
        private string raceURL = basicURL + "Race";
        private string seasonURL = basicURL + "Season";
        private string circuitURL = basicURL + "Circuit";
        public List<SeasonDTO> Seasons = new List<SeasonDTO>();
        public List<CircuitDTO> Circuits = new List<CircuitDTO>();

        public EditModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        // public long Id { get; set; }
        // public string Name { get; set; }
        // public int SeasonId { get; set; }
        // public int CircuitId { get; set; }
        // public DateTime StartDate { get; set; }
        // public DateTime EndDate { get; set; }
        public string startdateStr { get; set; }
        public string enddateStr { get; set; }
        public SelectList SeasonList { get; set; }
        public SelectList CircuitList { get; set; }

        [BindProperty] public RaceUpdateDTO Race { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            Seasons = await GetSeasons();
            SeasonList = new SelectList(Seasons, nameof(DAL.Models.Season.Id), nameof(DAL.Models.Season.Name));
            Circuits = await GetCircuits();
            CircuitList = new SelectList(Circuits, nameof(DAL.Models.Circuit.Id), nameof(DAL.Models.Circuit.Name));

            HttpResponseMessage response = await _client.GetAsync(raceURL + "?id=" + id);
            responseString = await response.Content.ReadAsStringAsync();
            Race = JsonSerializer.Deserialize<RaceUpdateDTO>(responseString, _options);
            SetDatesForUI();

            return Page();
        }

        private void SetDatesForUI()
        {
            startdateStr = Race.StartDate.ToString("yyyy-MM-dd");
            enddateStr = Race.EndDate.ToString("yyyy-MM-dd");
        }

        public async Task<List<SeasonDTO>> GetSeasons()
        {
            HttpResponseMessage response = await _client.GetAsync(seasonURL + "?items_per_page=50&page=0");
            responseString = await response.Content.ReadAsStringAsync();
            var seasonList = JsonConvert.DeserializeObject<List<SeasonDTO>>(responseString);


            foreach (SeasonDTO season in seasonList)
            {
                SeasonDTO addSeason = new SeasonDTO()
                {
                    Id = season.Id,
                    Name = season.Name
                };
                Seasons.Add(addSeason);
            }

            return Seasons;
        }

        public async Task<List<CircuitDTO>> GetCircuits()
        {
            HttpResponseMessage response = await _client.GetAsync(circuitURL + "?items_per_page=50&page=0");
            responseString = await response.Content.ReadAsStringAsync();
            var circuitList = JsonConvert.DeserializeObject<List<CircuitDTO>>(responseString);


            foreach (CircuitDTO circuit in circuitList)
            {
                CircuitDTO addCircuit = new CircuitDTO()
                {
                    Id = circuit.Id,
                    Name = circuit.Name
                };
                Circuits.Add(addCircuit);
            }

            return Circuits;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string jsonString = JsonSerializer.Serialize(Race);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(raceURL, stringContent);
            string requestRe = await response.Content.ReadAsStringAsync();

            responseString = requestRe;

            return Redirect("../race");
        }
    }
}