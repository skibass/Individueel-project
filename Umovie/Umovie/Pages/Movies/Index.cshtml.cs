using BLL;
using DALL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using System.Security.Cryptography.X509Certificates;

namespace Umovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        public Movie_Service movieService = new();
       
        public void OnGet()
        {
            
        }
        public IActionResult OnPostTryFavoriteMovie()
        {
            if (movieService.TryFavoriteMovie(int.Parse(Request.Form["movieId"]), (int)HttpContext.Session.GetInt32("uId")) == true)
            {
                
            }
            return Redirect("Movies/Index");
        }
    }
}
