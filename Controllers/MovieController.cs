using Microsoft.AspNetCore.Mvc;
using TPLab4.Data;
using TPLab4.Data.TPModels;

namespace TPLab4.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly TpLab4Context _context;
    public MovieController(TpLab4Context context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<Movie> GetMovies()
    {
        return _context.Movies.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Movie> GetById(int id)
    {
        var movie = _context.Movies.Find(id);

        if (movie is null)
        {
            return NotFound();
        }
        return Ok(movie);
    }

    [HttpPost]
    public IActionResult Create(Movie movie)
    {
        _context.Movies.Add(movie);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Movie movie)
    {
        if (id != movie.Id)
        {
            return BadRequest();
        }
        var existingMovie = _context.Movies.Find(id);

        if (existingMovie is null)
        {
            return NotFound();
        }

        existingMovie.MovieName = movie.MovieName;
        existingMovie.MovieGenre = movie.MovieGenre;
        existingMovie.MovieDuration = movie.MovieDuration;
        existingMovie.MovieBudget = movie.MovieBudget;

        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existingMovie = _context.Movies.Find(id);

        if (existingMovie is null)
        {
            return NotFound();
        }

        _context.Movies.Remove(existingMovie);
        _context.SaveChanges();

        return Ok();
    }

}