using BLL;
using DALL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace Umovie.Pages.Account
{
    public class LoginModel : PageModel
    {
        User_Service userService = new User_Service();
        public ErrorHandling errorHandling = new ErrorHandling();

        [BindProperty]
        public required User user { get; set; }
        public void OnGet()
        {
            user = new User();
        }
        public IActionResult OnPostTryLoginUser()
        {
            User loggedUser = userService.TryLoginUser(user);
            if (loggedUser != null)
            {
                HttpContext.Session.SetInt32("uId", loggedUser.UserId);
                HttpContext.Session.SetString("uName", loggedUser.UserName);
                HttpContext.Session.SetString("rName", loggedUser.Role.RoleName);

                return RedirectToPage("../Index");

            }
            else
            {
                errorHandling.Message = "Wrong username, email or password!";
                return Page();
            }
        }
    }
}
