using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Racing.DTO.ReadDTO;

namespace Racing.UI.Pages.Pilot
{
    public class IndexModel : PageModel
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _options;
        
        public string responseString;
        
        public List<PilotDTO> pilots = new List<PilotDTO>();
        
        private static string basicURL = "https://localhost:44397/api/";
        private string pilotURL = basicURL + "Pilot";

        public IndexModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        public async Task<List<PilotDTO>> GetPilots()
        {
            HttpResponseMessage response = await _client.GetAsync(pilotURL + "?items_per_page=50&page=0");
            responseString = await response.Content.ReadAsStringAsync();
            var pilotList = JsonSerializer.Deserialize<List<PilotDTO>>(responseString, _options);

            foreach (var pilot in pilotList)
            {
                PilotDTO addPilot = new PilotDTO()
                {
                    Id = pilot.Id,
                    LicensNr = pilot.LicensNr,
                    Name = pilot.Name,
                    FirstName = pilot.FirstName,
                    NickName = pilot.NickName,
                    PhotoPath = pilot.PhotoPath,
                    Gender = pilot.Gender,
                    Birthdate = pilot.Birthdate,
                    Length = pilot.Length,
                    Weight = pilot.Weight
                };
                pilots.Add(addPilot);
            }

            return pilots;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            pilots = await GetPilots();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            int id = int.Parse(Request.Form["Id"]);

            HttpResponseMessage response = await _client.GetAsync(pilotURL + "?id=" + id);
            string getResponse = await response.Content.ReadAsStringAsync();
            PilotDTO deletePilot = JsonSerializer.Deserialize<PilotDTO>(getResponse, _options);

            if (deletePilot.Id == id)
            {
                await _client.DeleteAsync(pilotURL + "/" + id);
            }

            responseString = getResponse;
            pilots = await GetPilots();
            return Page();
        }
    }
}