using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Racing.DTO.UpdateDTO;

namespace Racing.UI.Pages.Pilot
{
    public class EditModel : PageModel
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public string responseString;
        private static string basicURL = "https://localhost:44397/api/";
        private string pilotURL = basicURL + "Pilot";

        public EditModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        // public long Id { get; set; }
        // public string LicenseNr { get; set; }
        // public string Name { get; set; }
        // public string FirstName { get; set; }
        // public string NickName { get; set; }
        // public string PhotoPath { get; set; }
        // public string Gender { get; set; }
        // public DateTime Birthdate { get; set; }
        // public int Length { get; set; }
        // public decimal Weight { get; set; }

        [BindProperty] public PilotUpdateDTO Pilot { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            HttpResponseMessage response = await _client.GetAsync(pilotURL + "?id=" + id);
            responseString = await response.Content.ReadAsStringAsync();
            Pilot = JsonSerializer.Deserialize<PilotUpdateDTO>(responseString, _options);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string jsonString = JsonSerializer.Serialize(Pilot);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(pilotURL, stringContent);
            string requestRe = await response.Content.ReadAsStringAsync();

            responseString = requestRe;

            return Redirect("../pilot");
        }
    }
}