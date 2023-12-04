using BLL;
using DALL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace Umovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        public Movie_Service movieService = new();
       
        public void OnGet()
        {
            
        }
        [HttpPost("/form-body")]
        public IActionResult OnPostTryFavoriteMovie()
        {
            int movieId = int.Parse(Request.Form["movieId"]);
            int uId = (int)HttpContext.Session.GetInt32("uId");

            if (movieService.TryFavoriteMovie(movieId, uId) == true)
            {
                
            }
            return Page();
        }

        public IActionResult OnPostTryRateMovie()
        {
            if (Request.Form["rating"] != "")
            {
                int movieId = int.Parse(Request.Form["movieId"]);
                int rating = int.Parse(Request.Form["rating"]);
                int uId = (int)HttpContext.Session.GetInt32("uId");

                if (movieService.TryRateMovie(movieId, uId, rating) == true)
                {

                }
                return Page();
            }
            return null;
        }
    }
}
