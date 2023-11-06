using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace Pokemon_DB_Workshop.Pages.pokemon
{
    public class AddPokemonModel : PageModel
    {
        public Repository Repository = new();

        [BindProperty]
        public required Models.Pokemon pokemon { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //ModelState.AddModelError("exists", "The pokemon already exists");

            Repository.AddPokemon(pokemon);
            return RedirectToPage("/Index");
        }

        
    }
}
