using System;
using System.Collections.Generic;

namespace TPLab4..DataTPModels;

public partial class Movie
{
    public string? MovieName { get; set; }

    public string? MovieGenre { get; set; }

    public int? MovieDuration { get; set; }

    public decimal? MovieBudget { get; set; }
}
