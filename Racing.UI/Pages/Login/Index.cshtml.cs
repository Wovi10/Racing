using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Racing.UI.Pages.Login
{
    public class IndexModel : PageModel
    {
        [BindProperty] public Credential Credential { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
        }
    }

    public class Credential
    {
        // temp annotations
        [Required] [Display(Name = "Email")] public long Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}