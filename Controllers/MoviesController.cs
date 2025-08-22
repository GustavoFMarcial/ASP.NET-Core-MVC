using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCMovie.Data;
using MVCMovie.Models;
using MVCMovie.Services;
using SQLitePCL;

namespace MVCMovie.Controllers
{
    public class MoviesController : Controller
    {
        // private readonly MovieContext _context;
        private readonly ILogger<MoviesController> _logger;
        private readonly IMoviesService _movieService;

        public MoviesController(IMoviesService movieService, ILogger<MoviesController> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Usuário acessando pagina index");
            return View(await _movieService.GetListMoviesAsync());
            // return View(await _context.Movie.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            _movieService.AddMovie(movie);
            // _context.Movie.Add(movie);
            await _movieService.SaveChangesMoviesAsync();
            // await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie? movie = await _movieService.FindMovieAsync(id);
            // Movie? movie = await _context.Movie.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie? movie = await _movieService.FindMovieAsync(id);
            // Movie? movie = await _context.Movie.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            _logger.LogInformation("Usuário editando filme");

            if (id != movie.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            _movieService.UpdateMovie(movie);
            // _context.Update(movie);

            try
            {
                await _movieService.SaveChangesMoviesAsync();
                // await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                bool isValid = await MovieExist(id);

                if (!isValid)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie? movie = await _movieService.FindMovieAsync(id);
            // Movie? movie = await _context.Movie.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie? movie = await _movieService.FindMovieAsync(id);
            // Movie? movie = await _context.Movie.FindAsync(id);

            if (movie  == null)
            {
                return NotFound();
            }
            
            _movieService.RemoveMovie(movie);
            // _context.Movie.Remove(movie);

            await _movieService.SaveChangesMoviesAsync();
            // await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MovieExist(int? id)
        {
            return await _movieService.AnyMovieAsync(id);
        }
    }
}