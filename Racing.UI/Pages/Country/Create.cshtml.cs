using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Racing.DTO.CreateDTO;

namespace Racing.UI.Pages.Country
{
    public class CreateModel : PageModel
    {
        private HttpClient _client;

        public CreateModel(HttpClient client)
        {
            _client = client;
        }

        [BindProperty]
        public CountryCreateDTO Country { get; set; }

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
            // CountryCreateDTO countryCreateDto = new CountryCreateDTO()
            // {
            //     Name = Request.Form[nameof(Name)]
            // };
            
            string jsonString = JsonSerializer.Serialize(Country);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await _client.PostAsync("https://localhost:44397/api/country", stringContent);

            return Redirect("../country");
        }
    }
}