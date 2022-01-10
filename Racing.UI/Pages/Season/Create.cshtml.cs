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
using Racing.DTO.CreateDTO;
using Racing.DTO.ReadDTO;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Racing.UI.Pages.Season
{
    public class CreateModel : PageModel
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _options;
        
        private static string basicURL = "https://localhost:44397/api/";
        private string seasonURL = basicURL + "Season";
        private string seriesURL = basicURL + "Series";
        
        public string responseString;
        
        public List<SeriesDTO> Series = new List<SeriesDTO>();

        public CreateModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        public string Name { get; set; }
        public int SeriesId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool Active { get; set; }
        
        [BindProperty]
        public SeasonCreateDTO Season { get; set; }
        public SelectList SeriesList { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            Series = await GetSeries();
            SeriesList = new SelectList(Series, nameof(DAL.Models.Series.Id), nameof(DAL.Models.Series.Name));

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
            if (! ModelState.IsValid)
            {
                return Page();
            }
            
            // SeasonCreateDTO seasonCreateDto = new SeasonCreateDTO()
            // {
            //     Name = Request.Form[nameof(Name)],
            //     StartDate = DateTime.Parse(Request.Form[nameof(StartDate)]),
            //     EndDate = DateTime.Parse(Request.Form[nameof(EndDate)]),
            //     SeriesId = int.Parse(Request.Form[nameof(SeriesId)])
            // };
            Season.Active = IsSeasonActive(Season);
            string jsonString = JsonSerializer.Serialize(Season);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(seasonURL, stringContent);

            return Redirect("../season");
        }

        private static bool IsSeasonActive(SeasonCreateDTO season)
        {
            bool startBeforeToday = season.StartDate < DateTime.Today;
            bool endAfterToday = season.EndDate > DateTime.Today;

            bool result = (startBeforeToday && endAfterToday);

            return result;
        }
    }
}