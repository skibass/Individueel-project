using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TestAuthenticationMethod.umovie;

namespace TestAuthenticationMethod.Repos
{
    public class UserRepo
    {
        public List<User> GetUsers()
        {
            UmovieContext context = new UmovieContext();

            List<User> users = context.Users.Include(e => e.Role).Include(e => e.MovieRatings).ThenInclude(e => e.Movie).Include(e => e.UserFavoriteMovies).ThenInclude(e => e.Movie).ToList();

            if (users == null)
            {
                users = new List<User>();
            }
            return users;
        }
    }
}
