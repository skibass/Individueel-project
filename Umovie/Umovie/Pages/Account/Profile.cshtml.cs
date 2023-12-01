using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    }
}
