using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Models.Pagination;

namespace Umovie.Pages.Account
{
    public class ProfileModel : PageModel
    {
        public int uId { get; set; }
        public string uName { get; set; }
        public string uEmail { get; set; }
        public string uRole { get; set; }


        public Movie_Service movieService = new();
        public User_Service userService = new();

        public void OnGet()
        {
            int userId = (int)HttpContext.Session.GetInt32("uId");
            uId = userId;

            uName = userService.TryGetCurrentUser(uId).UserName;
            uEmail = userService.TryGetCurrentUser(uId).UserEmail;
            uRole = userService.TryGetCurrentUser(uId).Role.RoleName;
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

    }
}
