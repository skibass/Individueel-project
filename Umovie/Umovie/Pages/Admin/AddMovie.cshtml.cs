using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System.ComponentModel.DataAnnotations;

namespace Umovie.Pages.Admin
{
    public class AddMovie : PageModel
    {
        public Movie_Service Movie_Service = new();
        IWebHostEnvironment env;
        [BindProperty]
        public required Movie movie { get; set; }
        public IFormFile Upload {  get; set; }
        [BindProperty]
        public List<Category> categories { get; set; }
        [BindProperty]
        public List<string> chosenCategories { get; set; }

        public AddMovie(IWebHostEnvironment web)
        {
            env = web;
            categories = new List<Category>();

            this.categories = Movie_Service.TryGetCategories();
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("rName") != "admin")
            {
                if (HttpContext.Session.GetString("uId") == null)
                {
                    return RedirectToPage("../Account/Login");
                }
                return RedirectToPage("../Movies/Index");
            }
            return null;
        }
        public async Task<IActionResult> OnPostTryAddMovie()
        {

            if (ModelState.IsValid)
            {
                var file = Path.Combine(env.WebRootPath, "MovieImages");
                await Movie_Service.TryAddMovie(movie, chosenCategories, file, Upload);
                return RedirectToPage();
            }
            return null;
        }
    }
}
