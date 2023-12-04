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

        [BindProperty]
        public required Movie movie { get; set; }

        [BindProperty]
        public required MovieRating rating { get; set; }

        public void OnGet()
        {
            
        }
        public IActionResult OnPostTryFavoriteMovie()
        {
            int movieId = movie.MovieId;
            int uId = (int)HttpContext.Session.GetInt32("uId");

            if (movieService.TryFavoriteMovie(movieId, uId) == true)
            {
                
            }
            return RedirectToPage("../Movies/Index");
        }

        public IActionResult OnPostTryRateMovie()
        {
            if (Request.Form["rating"] != "")
            {
                int movieId = movie.MovieId;
                int ratingNumber = (int)rating.RatingNumber;
                int uId = (int)HttpContext.Session.GetInt32("uId");

                if (movieService.TryRateMovie(movieId, uId, ratingNumber) == true)
                {

                }
                return RedirectToPage("../Movies/Index");
            }
            return null;
        }
    }
}
