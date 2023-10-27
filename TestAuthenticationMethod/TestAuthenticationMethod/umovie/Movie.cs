using System;
using System.Collections.Generic;

namespace TestAuthenticationMethod.umovie;

public partial class Movie
{
    public int MovieId { get; set; }

    public string? MovieName { get; set; }

    public string? MovieDescription { get; set; }

    public string? MovieDirector { get; set; }

    public string? MovieLanguage { get; set; }

    public string? MovieReleaseDate { get; set; }

    public virtual ICollection<MovieRating> MovieRatings { get; set; } = new List<MovieRating>();

    public virtual ICollection<UserFavoriteMovie> UserFavoriteMovies { get; set; } = new List<UserFavoriteMovie>();
}
