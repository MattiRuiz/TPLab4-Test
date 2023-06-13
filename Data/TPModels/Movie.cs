using System;
using System.Collections.Generic;

namespace TPLab4.Data.TPModels;

public partial class Movie
{
    public int Id { get; set; }

    public string? MovieName { get; set; }

    public string? MovieGenre { get; set; }

    public int? MovieDuration { get; set; }

    public decimal? MovieBudget { get; set; }
}
