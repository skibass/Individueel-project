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
        //public User user = new();

        public Movie_Service movieService = new();
        public User_Service userService = new();
        public void OnGet()
        {
            int uId = (int)HttpContext.Session.GetInt32("uId");

            user = userService.TryGetCurrentUser(uId);
        }
        public IActionResult OnPostViewMovie()
        {
            HttpContext.Session.SetInt32("movieId", int.Parse(Request.Form["movieId"]));

            return RedirectToPage("../Movies/Movie");
        }
    }
}
