using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Umovie.Pages.Movies
{
    public class MovieModel : PageModel
    {
        public int Id { get; set; }
        public Movie_Service movieService = new();
        public void OnGet()
        {
            Id = (int)HttpContext.Session.GetInt32("movieId");
        }

    }
}
