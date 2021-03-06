using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Racing.DTO.UpdateDTO;

namespace Racing.UI.Pages.Series
{
    public class EditModel : PageModel
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _options;
        
        public string responseString;
        
        private static string basicURL = "https://localhost:44397/api/";
        private string seriesURL = basicURL + "Series";

        public EditModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        // public long Id { get; set; }
        // public string Name { get; set; }
        // public int SortingOrder { get; set; }
        // public DateTime StartDate { get; set; }
        // public DateTime EndDate { get; set; }
        // public bool Active { get; set; }
        public string startdateStr { get; set; }
        public string enddateStr { get; set; }
        [BindProperty] public SeriesUpdateDTO Series { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            HttpResponseMessage response = await _client.GetAsync(seriesURL + "?id=" + id);
            responseString = await response.Content.ReadAsStringAsync();
            Series = JsonSerializer.Deserialize<SeriesUpdateDTO>(responseString, _options);

            SetDatesForUI();
            
            return Page();
        }

        private void SetDatesForUI()
        {
            startdateStr = Series.StartDate.ToString("yyyy-MM-dd");
            enddateStr = Series.EndDate.ToString("yyyy-MM-dd");
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string jsonString = JsonSerializer.Serialize(Series);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(seriesURL, stringContent);
            string requestRe = await response.Content.ReadAsStringAsync();

            responseString = requestRe;

            return Redirect("../series");
        }
    }
}