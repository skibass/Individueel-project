using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace Umovie.Pages.Movies
{
    public class ViewRatingsModel : PageModel
    {
        [BindProperty]
        public required User user { get; set; }
        [BindProperty]
        public required Movie movie { get; set; }

        public Movie_Service movieService = new();
        public User_Service userService = new();
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("uId") == null)
            {
                return RedirectToPage("../Account/Login");
            }
            int uId = (int)HttpContext.Session.GetInt32("uId");

            user = userService.TryGetCurrentUser(uId);

            return null;
        }

        public IActionResult OnPostViewMovie()
        {
            HttpContext.Session.SetInt32("movieId", movie.MovieId);

            return RedirectToPage("../Movies/Movie");
        }
    }
}
