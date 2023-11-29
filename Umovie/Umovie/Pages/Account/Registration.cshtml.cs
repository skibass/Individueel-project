using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace Umovie.Pages.Account
{
    public class RegistrationModel : PageModel
    {
        User_Service userService = new User_Service();
        User user = new User();

        public void OnGet()
        {
        }
        public void OnPostTryRegisterUser()
        {
            user.UserEmail = Request.Form["userEmail"];
            user.UserName = Request.Form["userName"];

            userService.TryRegisterUser(user);
        }
    }
}
