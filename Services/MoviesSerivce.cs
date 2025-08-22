using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCMovie.Data;
using MVCMovie.Models;

namespace MVCMovie.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly MovieContext _context;

        public MoviesService(MovieContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetListMoviesAsync()
        {
            return await _context.Movie.ToListAsync();
        }

        public async Task<int> SaveChangesMoviesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Movie?> FindMovieAsync(int? id)
        {
            return await _context.Movie.FindAsync(id);
        }

        public async Task<bool> AnyMovieAsync(int? id)
        {
            return await _context.Movie.AnyAsync(e => e.Id == id);
        }

        public void AddMovie(Movie movie)
        {
            _context.Movie.Add(movie);
        }

        public void UpdateMovie(Movie movie)
        {
            _context.Movie.Update(movie);
        }

        public void RemoveMovie(Movie movie)
        {
            _context.Movie.Remove(movie);
        }
    }
}