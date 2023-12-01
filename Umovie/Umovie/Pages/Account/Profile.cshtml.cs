using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Models.Pagination;

namespace Umovie.Pages.Account
{
    public class ProfileModel : PageModel
    {
        public User user = new User();

        public Movie_Service movieService = new();
        public User_Service userService = new();

        public void OnGet()
        {
            int userId = (int)HttpContext.Session.GetInt32("uId");

            user = userService.TryGetCurrentUser(userId);
        }

        public IActionResult OnPostViewMovie()
        {
            HttpContext.Session.SetInt32("movieId", int.Parse(Request.Form["movieId"]));

            return RedirectToPage("../Movies/Movie");
        }
        public IActionResult OnPostTryUnfavoriteMovie()
        {
            if (movieService.TryFavoriteMovie(int.Parse(Request.Form["movieId"]), (int)HttpContext.Session.GetInt32("uId")) == true)
            {

            }
            return RedirectToPage("../Account/Profile");
        }
        public IActionResult OnPostTryRateMovie()
        {
            int movieId = int.Parse(Request.Form["movieId"]);
            int rating = int.Parse(Request.Form["rating"]);
            int uId = (int)HttpContext.Session.GetInt32("uId");

            if (movieService.TryRateMovie(movieId, uId, rating) == true)
            {

            }
            return RedirectToPage("../Account/Profile");
        }
    }
}
