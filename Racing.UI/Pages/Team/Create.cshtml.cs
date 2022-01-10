using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Racing.DTO.CreateDTO;

namespace Racing.UI.Pages.Team
{
    public class CreateModel : PageModel
    {
        private HttpClient _client;
        
        private static string basicURL = "https://localhost:44397/api/";
        private string teamURL = basicURL + "Team";

        public CreateModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
        }

        // public string Name { get; set; }
        [BindProperty]
        public TeamCreateDTO Team { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // TeamCreateDTO teamCreateDto = new TeamCreateDTO()
            // {
            //     Name = Request.Form[nameof(Name)]
            // };
            string jsonString = JsonSerializer.Serialize(Team);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(teamURL, stringContent);

            return Redirect("../team");
        }
    }
}