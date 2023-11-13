using System;
using System.Collections.Generic;

namespace Models;

public partial class MovieRating
{
    public int RatingId { get; set; }

    public int UserId { get; set; }

    public int? RatingNumber { get; set; }

    public int MovieId { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
