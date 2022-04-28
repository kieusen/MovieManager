using Movie_01.Data.Base;
using Movie_01.Data.ViewModels;
using Movie_01.Models;

namespace Movie_01.Data.Services
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task AddMovieSync (NewMovieVM movie);
        Task UpdateMovieSync (int id, NewMovieVM movie);

        Task<Movie> GetMovieByIdAsyn(int id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdownsVM();
    }
}