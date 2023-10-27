using System;
using System.Collections.Generic;

namespace TestAuthenticationMethod.umovie;

public partial class UserFavoriteMovie
{
    public int FavoriteId { get; set; }

    public int UserId { get; set; }

    public int MovieId { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
