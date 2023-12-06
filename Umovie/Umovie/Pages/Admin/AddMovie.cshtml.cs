using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace Umovie.Pages.Admin
{
    public class AddMovieModel : PageModel
    {
        public Movie_Service Movie_Service = new();
        IWebHostEnvironment env;
        [BindProperty]
        public required Movie movie { get; set; }
        public IFormFile Upload {  get; set; }

        public AddMovieModel(IWebHostEnvironment web)
        {
            env = web;
        }
        public IActionResult OnPostTryAddMovie()
        {
            var file = Path.Combine(env.WebRootPath, "MovieImages", Upload.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                Upload.CopyToAsync(fileStream);
            }
            movie.MovieImagePath = Upload.FileName;
            Movie_Service.AddMovie(movie);
            return RedirectToPage();
        }
    }
}
