using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Racing.DTO.CreateDTO;
using Racing.DTO.ReadDTO;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Racing.UI.Pages.Race
{
    public class CreateModel : PageModel
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _options;
        
        private static string basicURL = "https://localhost:44397/api/";
        private string raceURL = basicURL + "Race";
        private string seasonURL = basicURL + "Season";
        private string circuitURL = basicURL + "Circuit";
        
        public string responseString;
        
        public List<SeasonDTO> Seasons = new List<SeasonDTO>();
        public List<CircuitDTO> Circuits = new List<CircuitDTO>();

        public CreateModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        // public string Name { get; set; }
        // public int SeasonId { get; set; }
        // public int CircuitId { get; set; }
        // public string StartDate { get; set; }
        // public string EndDate { get; set; }
        public SelectList SeasonList { get; set; }
        public SelectList CircuitList { get; set; }
        
        [BindProperty]
        public RaceCreateDTO Race { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            Seasons = await GetSeasons();
            SeasonList = new SelectList(Seasons, nameof(DAL.Models.Season.Id), nameof(DAL.Models.Season.Name));
            Circuits = await GetCircuits();
            CircuitList = new SelectList(Circuits, nameof(DAL.Models.Circuit.Id), nameof(DAL.Models.Circuit.Name));

            return Page();
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
            // RaceCreateDTO raceCreateDto = new RaceCreateDTO()
            // {
            //     Name = Request.Form[nameof(Name)],
            //     StartDate = DateTime.Parse(Request.Form[nameof(StartDate)]),
            //     EndDate = DateTime.Parse(Request.Form[nameof(EndDate)]),
            //     SeasonId = int.Parse(Request.Form[nameof(SeasonId)]),
            //     CircuitId = int.Parse(Request.Form[nameof(CircuitId)])
            // };
            string jsonString = JsonSerializer.Serialize(Race);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(raceURL, stringContent);

            return Redirect("../race");
        }
    }
}