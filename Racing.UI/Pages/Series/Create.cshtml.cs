using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Racing.DTO.CreateDTO;

namespace Racing.UI.Pages.Series
{
    public class CreateModel : PageModel
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _options;
        
        private static string basicURL = "https://localhost:44397/api/";
        private string seriesURL = basicURL + "Series";

        public CreateModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }
        
        [BindProperty]
        public SeriesCreateDTO Series { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (! ModelState.IsValid)
            {
                return Page();
            }
            
            Series.Active = IsSeasonActive(Series);
            string jsonString = JsonSerializer.Serialize(Series);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(seriesURL, stringContent);

            return Redirect("../series");
        }

        private static bool IsSeasonActive(SeriesCreateDTO series)
        {
            bool startBeforeToday = series.StartDate < DateTime.Today;
            bool endAfterToday = series.EndDate > DateTime.Today;

            bool result = (startBeforeToday && endAfterToday);

            return result;
        }
    }
}