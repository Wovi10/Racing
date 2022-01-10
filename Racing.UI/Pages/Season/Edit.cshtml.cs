using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Racing.DTO.ReadDTO;
using Racing.DTO.UpdateDTO;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Racing.UI.Pages.Season
{
    public class EditModel : PageModel
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _options;
        
        public string responseString;
        
        private static string basicURL = "https://localhost:44397/api/";
        private string seasonURL = basicURL + "Season";
        private string seriesURL = basicURL + "Series";
        
        public List<SeriesDTO> Series = new List<SeriesDTO>();

        public EditModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        // public long Id { get; set; }
        // public string Name { get; set; }
        // public int SeriesId { get; set; }
        // public int CircuitId { get; set; }
        // public DateTime StartDate { get; set; }
        // public DateTime EndDate { get; set; }
        public SelectList SeriesList { get; set; }

        [BindProperty] 
        public SeasonUpdateDTO Season { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            Series = await GetSeries();
            SeriesList = new SelectList(Series, nameof(DAL.Models.Series.Id), nameof(DAL.Models.Series.Name));

            HttpResponseMessage response = await _client.GetAsync(seasonURL + "?id=" + id);
            responseString = await response.Content.ReadAsStringAsync();
            Season = JsonSerializer.Deserialize<SeasonUpdateDTO>(responseString, _options);

            return Page();
        }

        public async Task<List<SeriesDTO>> GetSeries()
        {
            HttpResponseMessage response = await _client.GetAsync(seriesURL + "?items_per_page=50&page=0");
            responseString = await response.Content.ReadAsStringAsync();
            var seriesList = JsonConvert.DeserializeObject<List<SeriesDTO>>(responseString);


            foreach (SeriesDTO series in seriesList)
            {
                SeriesDTO addSeries = new SeriesDTO()
                {
                    Id = series.Id,
                    Name = series.Name
                };
                Series.Add(addSeries);
            }

            return Series;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string jsonString = JsonSerializer.Serialize(Season);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(seasonURL, stringContent);
            string requestRe = await response.Content.ReadAsStringAsync();

            responseString = requestRe;

            return Redirect("../season");
        }
    }
}