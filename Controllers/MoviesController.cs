using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCMovie.Data;
using MVCMovie.Models;

namespace MVCMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieContext _context;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(MovieContext context, ILogger<MoviesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Usuário acessando pagina index");
            return View(await _context.Movie.ToListAsync());
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

            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie? movie = await _context.Movie.FindAsync(id);

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

            Movie? movie = await _context.Movie.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            _logger.LogInformation("Usuário editando filme");
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            _context.Update(movie);

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                if (!MovieExist(movie.Id))
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

            Movie? movie = await _context.Movie.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            Movie? movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExist(int id)
        {
            Movie? movie = _context.Movie.Find(id);

            if (movie == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // public async Task<IActionResult> Delete(int? id)
        // {

        // }
    }
}