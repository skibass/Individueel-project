using DALL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;

namespace Umovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        public Movie_Repository Movie_Repository = new();


        public void OnGet()
        {
            
        }

    }
}
