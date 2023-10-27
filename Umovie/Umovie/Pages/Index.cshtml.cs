using DALL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace Umovie.Pages
{
    public class IndexModel : PageModel
    {
        public User_Repository User_Repository= new User_Repository();
        Movie_Repository movie_Repository= new Movie_Repository(); 
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            //movie_Repository.DeleteAsFavorite(6);
        }
    }
}