using DALL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;

namespace Database_structuur_testomgeving.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public Movie_Repository MovieRepository = new();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        
        }
        public void OnPostTryUpdateMovie()
        {
            int movieId = int.Parse(Request.Form["movieId"]);
            string movieName = Request.Form["movieName"];

               MovieRepository.UpdateMovie(movieId, movieName);    
        }

        public void OnPostTryDeleteMovie()
        {
            int movieId = int.Parse(Request.Form["movieId"]);
            
                MovieRepository.DeleteMovie(movieId);
            
        }
    }
}