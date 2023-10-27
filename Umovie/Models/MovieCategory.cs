using System;
using System.Collections.Generic;

namespace Models;

public partial class MovieCategory
{
    public int Id { get; set; }

    public int MovieId { get; set; }

    public int CategorieId { get; set; }

    public virtual Category Categorie { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
