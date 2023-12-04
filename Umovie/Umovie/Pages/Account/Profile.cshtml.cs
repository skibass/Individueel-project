using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Models.Pagination;

namespace Umovie.Pages.Account
{
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public required User user { get; set; }

        [BindProperty]
        public required Movie movie { get; set; }

        [BindProperty]
        public required MovieRating rating { get; set; }
        //public User user = new User();

        public Movie_Service movieService = new();
        public User_Service userService = new();

        public void OnGet()
        {
            int userId = (int)HttpContext.Session.GetInt32("uId");

            user = userService.TryGetCurrentUser(userId);
        }

        public IActionResult OnPostViewMovie()
        {
            HttpContext.Session.SetInt32("movieId", movie.MovieId);

            return RedirectToPage("../Movies/Movie");
        }
        public IActionResult OnPostTryUnfavoriteMovie()
        {
            int movieId = movie.MovieId;
            int uId = (int)HttpContext.Session.GetInt32("uId");

            if (movieService.TryFavoriteMovie(movieId, uId) == true)
            {

            }
            return RedirectToPage("../Account/Profile");
        }
        public IActionResult OnPostTryRateMovie()
        {
            int movieId = movie.MovieId;
            int ratingNumber = (int)this.rating.RatingNumber;
            int uId = (int)HttpContext.Session.GetInt32("uId");

            if (movieService.TryRateMovie(movieId, uId, ratingNumber) == true)
            {

            }
            return RedirectToPage("../Account/Profile");
        }

        public IActionResult OnPostTryViewRatings()
        {
            HttpContext.Session.SetInt32("uId", (int)HttpContext.Session.GetInt32("uId"));

            return RedirectToPage("../Movies/ViewRatings");
        }
    }
}
