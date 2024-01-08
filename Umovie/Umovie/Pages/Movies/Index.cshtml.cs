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

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("uId") == null)
            {
                return RedirectToPage("../Account/Login");
            }
            return null;
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
        public IActionResult OnPostEditMovie()
        {
            int movieId = movie.MovieId;

            HttpContext.Session.SetInt32("editMovieId", movieId);

            return RedirectToPage("../Admin/EditMovie");

        }
        public IActionResult OnPostDeleteMovie()
        {
            int movieId = movie.MovieId;

            movieService.DeleteMovie(movieId);
            return RedirectToPage();
        }
        public IActionResult OnPostViewMovie()
        {
            HttpContext.Session.SetInt32("movieId", movie.MovieId);

            return RedirectToPage("../Movies/Movie");
        }
    }
}
