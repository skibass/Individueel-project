using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace Umovie.Pages.Account
{
    public class RegistrationModel : PageModel
    {
        User_Service userService = new User_Service();

        [BindProperty]
        public User user { get; set; }

        public void OnGet()
        {
            user = new User();
        }
        public IActionResult OnPostTryRegisterUser()
        {
            if (userService.TryRegisterUser(user))
            {
                User loggedUser = userService.TryLoginUser(user);
                if (loggedUser != null)
                {
                    HttpContext.Session.SetInt32("uId", loggedUser.UserId);
                    HttpContext.Session.SetString("uName", loggedUser.UserName);
                }
            }
            return RedirectToPage("../Index");
        }
    }
}
