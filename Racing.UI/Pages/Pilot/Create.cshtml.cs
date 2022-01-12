using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Racing.DTO.CreateDTO;

namespace Racing.UI.Pages.Pilot
{
    public class CreateModel : PageModel
    {
        private HttpClient _client;
        
        private static string basicURL = "https://localhost:44397/api/";
        private string pilotURL = basicURL + "Pilot";

        public CreateModel(HttpClient client)
        {
            _client = client;
        }
        
        [BindProperty]
        public PilotCreateDTO Pilot { get; set; }

        public IActionResult OnGet()
        {
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

            HttpResponseMessage response = await _client.PostAsync(pilotURL, stringContent);

            return Redirect("../pilot");
        }
    }
}