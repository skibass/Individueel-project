using BLL;
using DALL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace Umovie.Pages
{
    public class IndexModel : PageModel
    {
        public User_Service userService= new User_Service();

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