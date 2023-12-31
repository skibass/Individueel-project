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
        public void OnGet()
        {
            Id = (int)HttpContext.Session.GetInt32("movieId");
            int uId = (int)HttpContext.Session.GetInt32("uId");

            user = userService.TryGetCurrentUser(uId);
        }
        public IActionResult OnPostTryRateMovie()
        {
            int movieId = movie.MovieId;
            int ratingNumber = (int)rating.RatingNumber;
            int uId = (int)HttpContext.Session.GetInt32("uId");

            if (movieService.TryRateMovie(movieId, uId, ratingNumber) == true)
            {

            }
            return RedirectToPage("../Movies/Movie");
        }
    }
}
