using MVCMovie.Models;
using SQLitePCL;

namespace MVCMovie.Services
{
    public interface IMoviesService
    {
        public Task<List<Movie>> GetListMoviesAsync();

        public Task<int> SaveChangesMoviesAsync();

        public Task<Movie?> FindMovieAsync(int? id);

        public Task<bool> AnyMovieAsync(int? id);

        public void AddMovie(Movie movie);

        public void UpdateMovie(Movie movie);

        public void RemoveMovie(Movie movie);
    }
}