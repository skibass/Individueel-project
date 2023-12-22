using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace Umovie.Pages.Admin
{
    public class EditMovieModel : PageModel
    {
        public Movie_Service Movie_Service = new();
        IWebHostEnvironment env;
        [BindProperty]
        public required Movie movie { get; set; }
        public IFormFile Upload { get; set; }
        [BindProperty]
        public List<Category> categories { get; set; }
        [BindProperty]
        public List<string> chosenCategories { get; set; }

        public EditMovieModel(IWebHostEnvironment web)
        {
            env = web;
            categories = new List<Category>();

            this.categories = Movie_Service.TryGetCategories();
            
        }

        public IActionResult OnGet()
        {
            int mId = (int)HttpContext.Session.GetInt32("editMovieId");
            movie = Movie_Service.TryGetMovie(mId);

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
        public async Task<IActionResult> OnPostTryEditMovie()
        {
            if (ModelState.IsValid)
            {
                int mId = (int)HttpContext.Session.GetInt32("editMovieId");
                movie = Movie_Service.TryGetMovie(mId);
                var file = Path.Combine(env.WebRootPath, "MovieImages");
                await Movie_Service.TryEditMovie(movie, chosenCategories, file, Upload);
                return RedirectToPage("../Movies/Index");
            }
            return null;
        }
    }
}
