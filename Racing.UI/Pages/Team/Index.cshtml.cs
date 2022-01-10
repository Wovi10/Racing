using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Racing.DTO.ReadDTO;

namespace Racing.UI.Pages.Team
{
    public class IndexModel : PageModel
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public string responseString;
        public List<TeamDTO> Teams = new List<TeamDTO>();
        private static string basicURL = "https://localhost:44397/api/";
        private string teamURL = basicURL + "Team";

        public IndexModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        public async Task<List<TeamDTO>> GetTeams()
        {
            HttpResponseMessage response = await _client.GetAsync(teamURL + "?items_per_page=50&page=0");
            responseString = await response.Content.ReadAsStringAsync();
            var teamList = JsonSerializer.Deserialize<List<TeamDTO>>(responseString, _options);

            foreach (var team in teamList)
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

        public async Task<IActionResult> OnGetAsync()
        {
            Teams = await GetTeams();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            int id = int.Parse(Request.Form["Id"]);

            HttpResponseMessage response = await _client.GetAsync(teamURL + "?id=" + id);
            string getResponse = await response.Content.ReadAsStringAsync();
            TeamDTO deleteSeries = JsonSerializer.Deserialize<TeamDTO>(getResponse, _options);
            
            if (deleteSeries.Id == id)
            {
                await _client.DeleteAsync(teamURL + "/" + id);
            }

            responseString = getResponse;
            Teams = await GetTeams();
            return Page();
        }
    }
}