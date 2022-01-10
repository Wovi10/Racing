using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Racing.DTO.UpdateDTO;

namespace Racing.UI.Pages.Team
{
    public class EditModel : PageModel
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _options;
        
        public string responseString;
        
        private static string basicURL = "https://localhost:44397/api/";
        private string teamURL = basicURL + "Team";

        public EditModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        // public long Id { get; set; }
        // public string Name { get; set; }

        [BindProperty] 
        public TeamUpdateDTO Team { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            HttpResponseMessage response = await _client.GetAsync(teamURL + "?id=" + id);
            responseString = await response.Content.ReadAsStringAsync();
            Team = JsonSerializer.Deserialize<TeamUpdateDTO>(responseString, _options);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string jsonString = JsonSerializer.Serialize(Team);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(teamURL, stringContent);
            string requestRe = await response.Content.ReadAsStringAsync();

            responseString = requestRe;

            return Redirect("../team");
        }
    }
}