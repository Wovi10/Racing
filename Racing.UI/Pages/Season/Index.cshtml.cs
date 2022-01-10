using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Racing.DTO.ReadDTO;

namespace Racing.UI.Pages.Season
{
    public class IndexModel : PageModel
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public string responseString;
        public List<SeasonDTO> seasons = new List<SeasonDTO>();
        private static string basicURL = "https://localhost:44397/api/";
        private string seasonURL = basicURL + "Season";
        private string seriesURL = basicURL + "Series";

        public IndexModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        public async Task<List<SeasonDTO>> GetSeasons()
        {
            HttpResponseMessage response = await _client.GetAsync(seasonURL + "?items_per_page=50&page=0");
            responseString = await response.Content.ReadAsStringAsync();
            var seasonList = JsonSerializer.Deserialize<List<SeasonDTO>>(responseString, _options);

            foreach (var season in seasonList)
            {
                SeasonDTO addSeason = new SeasonDTO()
                {
                    Id = season.Id,
                    Name = season.Name,
                    StartDate = season.StartDate,
                    EndDate = season.EndDate,
                    SeriesId = season.SeriesId,
                    Active = season.Active
                };
                seasons.Add(addSeason);
            }

            return seasons;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            seasons = await GetSeasons();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            int id = int.Parse(Request.Form["Id"]);

            HttpResponseMessage response = await _client.GetAsync(seasonURL + "?id=" + id);
            string getResponse = await response.Content.ReadAsStringAsync();
            SeasonDTO deleteSeason = JsonSerializer.Deserialize<SeasonDTO>(getResponse, _options);

            if (deleteSeason.Id == id)
            {
                await _client.DeleteAsync(seasonURL + "/" + id);
            }

            responseString = getResponse;
            seasons = await GetSeasons();
            return Page();
        }

        public async Task<string> GetSeriesName(int itemSeriesId)
        {
            HttpResponseMessage response = await _client.GetAsync(seriesURL + "?id=" + itemSeriesId);
            responseString = await response.Content.ReadAsStringAsync();
            SeriesDTO series = JsonSerializer.Deserialize<SeriesDTO>(responseString, _options);
            string seriesName = series.Name;

            return seriesName;
        }
    }
}