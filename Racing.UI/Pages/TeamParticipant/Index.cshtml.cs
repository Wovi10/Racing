using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Racing.DTO.ReadDTO;

namespace Racing.UI.Pages.TeamParticipant
{
    public class IndexModel : PageModel
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public string responseString;
        public List<TeamParticipantsDTO> teamParticipants = new List<TeamParticipantsDTO>();
        private static string basicURL = "https://localhost:44397/api/";
        private string tpURL = basicURL + "TeamParticipant";
        private string teamURL = basicURL + "Team";
        private string raceURL = basicURL + "Race";
        private string pilotURL = basicURL + "Pilot";

        public IndexModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        public async Task<List<TeamParticipantsDTO>> GetTeamParticipants()
        {
            HttpResponseMessage response = await _client.GetAsync(tpURL + "?items_per_page=50&page=0");
            responseString = await response.Content.ReadAsStringAsync();
            var tpList = JsonSerializer.Deserialize<List<TeamParticipantsDTO>>(responseString, _options);

            foreach (var tp in tpList)
            {
                TeamParticipantsDTO addTp = new TeamParticipantsDTO()
                {
                    Id = tp.Id,
                    TeamId = tp.TeamId,
                    RaceId = tp.RaceId,
                    PilotId = tp.PilotId
                };
                teamParticipants.Add(addTp);
            }

            return teamParticipants;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            teamParticipants = await GetTeamParticipants();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            int id = int.Parse(Request.Form["Id"]);

            HttpResponseMessage response = await _client.GetAsync(tpURL + "?id=" + id);
            string getResponse = await response.Content.ReadAsStringAsync();
            TeamParticipantsDTO deleteTp = JsonSerializer.Deserialize<TeamParticipantsDTO>(getResponse, _options);

            if (deleteTp.Id == id)
            {
                await _client.DeleteAsync(tpURL + "/" + id);
            }

            responseString = getResponse;
            teamParticipants = await GetTeamParticipants();
            return Page();
        }

        public async Task<string> GetTeamName(int itemTeamId)
        {
            HttpResponseMessage response = await _client.GetAsync(teamURL + "?id=" + itemTeamId);
            responseString = await response.Content.ReadAsStringAsync();
            TeamDTO team = JsonSerializer.Deserialize<TeamDTO>(responseString, _options);
            string teamName = team.Name;

            return teamName;
        }

        public async Task<string> GetRaceName(int itemRaceId)
        {
            HttpResponseMessage response = await _client.GetAsync(raceURL + "?id=" + itemRaceId);
            responseString = await response.Content.ReadAsStringAsync();
            RaceDTO race = JsonSerializer.Deserialize<RaceDTO>(responseString, _options);
            string raceName = race.Name;

            return raceName;
        }
        
        public async Task<string> GetPilotName(int itemPilotId)
        {
            HttpResponseMessage response = await _client.GetAsync(pilotURL + "?id=" + itemPilotId);
            responseString = await response.Content.ReadAsStringAsync();
            PilotDTO pilot = JsonSerializer.Deserialize<PilotDTO>(responseString, _options);
            string pilotName = pilot.Name;

            return pilotName;
        }
    }
}