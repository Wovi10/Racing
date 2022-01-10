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

        // public string LicensNr { get; set; }
        // public string Name { get; set; }
        // public string FirstName { get; set; }
        // public string NickName { get; set; }
        // public string PhotoPath { get; set; }
        // public string Gender { get; set; }
        // public DateTime Birthdate { get; set; }
        // public int Length { get; set; }
        // public decimal Weight { get; set; }
        [BindProperty]
        public PilotCreateDTO Pilot { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (! ModelState.IsValid)
            {
                return Page();
            }
            
            // PilotCreateDTO pilotDTO = new PilotCreateDTO
            // {
            //     LicensNr = Request.Form[nameof(LicensNr)],
            //     Name = Request.Form[nameof(Name)],
            //     FirstName = Request.Form[nameof(FirstName)],
            //     NickName = Request.Form[nameof(NickName)],
            //     PhotoPath = Request.Form[nameof(PhotoPath)],
            //     Gender = Request.Form[nameof(Gender)],
            //     Birthdate = DateTime.Parse(Request.Form[nameof(Birthdate)]),
            //     Length = int.Parse(Request.Form[nameof(Length)]),
            //     Weight = decimal.Parse(Request.Form[nameof(Weight)]),
            // };

            string jsonString = JsonSerializer.Serialize(Pilot);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(pilotURL, stringContent);

            return Redirect("../pilot");
        }
    }
}