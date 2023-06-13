using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPLab4.Controllers;
using TPLab4.Data;
using TPLab4.Data.TPModels;
using Xunit;

namespace TPLab4.Tests
{
    public class MovieControllerTests
    {
        private readonly TpLab4Context _context;

        public MovieControllerTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("TPConnection");

            var options = new DbContextOptionsBuilder<TpLab4Context>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new TpLab4Context(options);
        }

        [Fact]
        public void GetMovies_DevuelveUnaListaDePeliculas()
        {
            // Arrange
            var controller = new MovieController(_context);

            // Act
            var resultado = controller.GetMovies();

            // Assert
            var movies = Assert.IsAssignableFrom<IEnumerable<Movie>>(resultado);

            // Verifica que se hayan devuelto películas
            Assert.NotEmpty(movies);
        }

        [Fact]
        public void GetById_ExisteId_DevuelvePelicula()
        {
            // Arrange
            var controller = new MovieController(_context);
            var movieId = 1;

            // Act
            var resultado = controller.GetById(movieId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Movie>>(resultado);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var movie = Assert.IsAssignableFrom<Movie>(okObjectResult.Value);

            // Verifica que se haya devuelto la película correcta
            Assert.Equal(movieId, movie.Id);
        }

        [Fact]
        public void GetById_NoExisteId_DevuelveNotFound()
        {
            // Arrange
            var controller = new MovieController(_context);
            var nonExistingId = 100;

            // Act
            var result = controller.GetById(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Create_EsPosibleCrearUnaPelicula()
        {
            // Arrange
            var controller = new MovieController(_context);
            var movie = new Movie
            {
                MovieName = "Test Movie",
                MovieGenre = "Test Genre",
                MovieDuration = 120,
                MovieBudget = 1000000
            };

            // Act
            var result = controller.Create(movie);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);

            controller.Delete(movie.Id);

        }

        [Fact]
        public void Update_EsPosibleActualizarUnaPelicula()
        {
            // Arrange
            var controller = new MovieController(_context);
            var movie = new Movie
            {
                MovieName = "Test Movie",
                MovieGenre = "Test Genre",
                MovieDuration = 120,
                MovieBudget = 1000000
            };

            controller.Create(movie);

            var updatedMovie = new Movie
            {
                Id = movie.Id,
                MovieName = "Updated Movie",
                MovieGenre = "Updated Genre",
                MovieDuration = 150,
                MovieBudget = 2000000
            };

            // Act
            var result = controller.Update(movie.Id, updatedMovie);

            // Assert
            Assert.IsType<NoContentResult>(result);

            controller.Delete(movie.Id);
        }

        [Fact]
        public void Delete_EsPosibleBorrarUnaPelicula()
        {
            // Arrange
            var controller = new MovieController(_context);
            var movie = new Movie
            {
                MovieName = "Deleted Movie",
                MovieGenre = "Deleted Genre",
                MovieDuration = 120,
                MovieBudget = 1000000
            };

            controller.Create(movie);

            // Act
            var result = controller.Delete(movie.Id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

    }
}
