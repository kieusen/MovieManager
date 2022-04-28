using MovieManager.Data.Base;
using MovieManager.Data.ViewModels;
using MovieManager.Models;

namespace MovieManager.Data.Services
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task AddMovieSync (NewMovieVM movie);
        Task UpdateMovieSync (int id, NewMovieVM movie);

        Task<Movie> GetMovieByIdAsyn(int id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdownsVM();
    }
}