using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace Umovie.Pages.Movies
{
    public class MovieModel : PageModel
    {
        public int Id { get; set; }
        [BindProperty]
        public required User user { get; set; }

        [BindProperty]
        public required Movie movie { get; set; }

        [BindProperty]
        public required MovieRating rating { get; set; }

        public Movie_Service movieService = new();
        public User_Service userService = new();
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("uId") == null)
            {
                return RedirectToPage("../Account/Login");
            }
            Id = (int)HttpContext.Session.GetInt32("movieId");
            int uId = (int)HttpContext.Session.GetInt32("uId");

            user = userService.TryGetCurrentUser(uId);
            return null;
        }
        public IActionResult OnPostTryRateMovie()
        {
            int movieId = movie.MovieId;
            int ratingNumber = (int)rating.RatingNumber;
            int uId = (int)HttpContext.Session.GetInt32("uId");

            movieService.TryRateMovie(movieId, uId, ratingNumber);

            return RedirectToPage("../Movies/Movie");
        }
    }
}
