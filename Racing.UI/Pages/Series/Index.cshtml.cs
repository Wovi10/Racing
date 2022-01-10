using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Racing.DTO.ReadDTO;

namespace Racing.UI.Pages.Series
{
    public class IndexModel : PageModel
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public string responseString;
        public List<SeriesDTO> Series = new List<SeriesDTO>();
        private static string basicURL = "https://localhost:44397/api/";
        private string seriesURL = basicURL + "Series";

        public IndexModel(HttpClient client, JsonSerializerOptions options)
        {
            _client = client;
            _options = options;
        }

        public async Task<List<SeriesDTO>> GetSeries()
        {
            HttpResponseMessage response = await _client.GetAsync(seriesURL + "?items_per_page=50&page=0");
            responseString = await response.Content.ReadAsStringAsync();
            var seriesList = JsonSerializer.Deserialize<List<SeriesDTO>>(responseString, _options);

            foreach (var series in seriesList)
            {
                SeriesDTO addSeries = new SeriesDTO()
                {
                    Id = series.Id,
                    Name = series.Name,
                    StartDate = series.StartDate,
                    EndDate = series.EndDate,
                    Active = series.Active,
                    SortingOrder = series.SortingOrder
                };
                Series.Add(addSeries);
            }

            return Series;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Series = await GetSeries();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            int id = int.Parse(Request.Form["Id"]);

            HttpResponseMessage response = await _client.GetAsync(seriesURL + "?id=" + id);
            string getResponse = await response.Content.ReadAsStringAsync();
            SeriesDTO deleteSeries = JsonSerializer.Deserialize<SeriesDTO>(getResponse, _options);

            if (deleteSeries.Id == id)
            {
                await _client.DeleteAsync(seriesURL + "/" + id);
            }

            responseString = getResponse;
            Series = await GetSeries();
            return Page();
        }
    }
}