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

namespace Racing.UI.Pages.TeamParticipant
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        private string _responseString;
        
        private static string basicURL = "https://localhost:44397/api/";
        private readonly string tpURL = basicURL + "TeamParticipant";
        private readonly string teamURL = basicURL + "Team";
        private readonly string raceURL = basicURL + "Race";
        private readonly string pilotURL = basicURL + "Pilot";

        private List<TeamDTO> Teams = new List<TeamDTO>();
        private List<RaceDTO> Races = new List<RaceDTO>();
        private List<PilotDTO> Pilots = new List<PilotDTO>();

        public EditModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }
        
        public SelectList TeamList { get; set; }
        public SelectList RaceList { get; set; }
        public SelectList PilotList { get; set; }

        [BindProperty] public TeamParticipantsUpdateDTO Tp { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            
            Teams = await GetTeams();
            TeamList = new SelectList(Teams, nameof(DAL.Models.Team.Id), nameof(DAL.Models.Team.Name));
            Pilots = await GetPilots();
            PilotList = new SelectList(Pilots, nameof(DAL.Models.Pilot.Id), nameof(DAL.Models.Pilot.Name));
            Races = await GetRaces();
            RaceList = new SelectList(Races, nameof(DAL.Models.Race.Id), nameof(DAL.Models.Race.Name));

            HttpResponseMessage response = await _client.GetAsync(tpURL + "?id=" + id);
            _responseString = await response.Content.ReadAsStringAsync();
            Tp = JsonSerializer.Deserialize<TeamParticipantsUpdateDTO>(_responseString, _options);

            return Page();
        }
        
        public async Task<List<TeamDTO>> GetTeams()
        {
            HttpResponseMessage response = await _client.GetAsync(teamURL + "?items_per_page=50&page=0");
            _responseString = await response.Content.ReadAsStringAsync();
            var teamList = JsonConvert.DeserializeObject<List<TeamDTO>>(_responseString);


            foreach (TeamDTO team in teamList)
            {
                TeamDTO addTeam = new TeamDTO()
                {
                    Id = team.Id,
                    Name = team.Name
                };
                Teams.Add(addTeam);
            }

            return Teams;
        }

        public async Task<List<RaceDTO>> GetRaces()
        {
            HttpResponseMessage response = await _client.GetAsync(raceURL + "?items_per_page=50&page=0");
            _responseString = await response.Content.ReadAsStringAsync();
            var raceList = JsonConvert.DeserializeObject<List<RaceDTO>>(_responseString);


            foreach (RaceDTO race in raceList)
            {
                RaceDTO addRace = new RaceDTO()
                {
                    Id = race.Id,
                    Name = race.Name
                };
                Races.Add(addRace);
            }

            return Races;
        }

        public async Task<List<PilotDTO>> GetPilots()
        {
            HttpResponseMessage response = await _client.GetAsync(pilotURL + "?items_per_page=50&page=0");
            _responseString = await response.Content.ReadAsStringAsync();
            var pilotList = JsonConvert.DeserializeObject<List<PilotDTO>>(_responseString);


            foreach (PilotDTO pilot in pilotList)
            {
                PilotDTO addPilot = new PilotDTO()
                {
                    Id = pilot.Id,
                    Name = pilot.Name
                };
                Pilots.Add(addPilot);
            }

            return Pilots;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string jsonString = JsonSerializer.Serialize(Tp);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(tpURL, stringContent);
            string requestRe = await response.Content.ReadAsStringAsync();

            _responseString = requestRe;

            return Redirect("../teamparticipant");
        }
    }
}