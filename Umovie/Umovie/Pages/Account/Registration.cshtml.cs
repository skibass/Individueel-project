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
        public ErrorHandling errorHandling = new ErrorHandling();

        [BindProperty]
        public required User user { get; set; }

        public void OnGet()
        {
            //user = new User();
        }
        public IActionResult OnPostTryRegisterUser()
        {
            User newUser = userService.TryRegisterUser(user);
            if (newUser != null)
            {
                HttpContext.Session.SetInt32("uId", newUser.UserId);
                HttpContext.Session.SetString("uName", newUser.UserName);
                HttpContext.Session.SetString("rName", newUser.Role.RoleName);

                return RedirectToPage("../Index");
            }
            else
            {
                errorHandling.Message = "A user with this email adres already exists!";
                return Page();
            }
        }
    }
}
