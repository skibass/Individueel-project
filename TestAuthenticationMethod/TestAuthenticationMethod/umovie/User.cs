using System;
using System.Collections.Generic;

namespace TestAuthenticationMethod.umovie;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserEmail { get; set; }

    public int RoleId { get; set; }

    public virtual ICollection<MovieRating> MovieRatings { get; set; } = new List<MovieRating>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<UserFavoriteMovie> UserFavoriteMovies { get; set; } = new List<UserFavoriteMovie>();
}
