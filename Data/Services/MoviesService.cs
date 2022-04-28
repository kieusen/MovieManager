using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieManager.Data.Base;
using MovieManager.Data.ViewModels;
using MovieManager.Models;

namespace MovieManager.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public MoviesService(AppDbContext context, IMapper mapper, IWebHostEnvironment env) : base(context, env)
        {
            _env = env;
            _mapper = mapper;
            _context = context;
        }

        public async Task AddMovieSync(NewMovieVM movie)
        {
            var newMovie = _mapper.Map<NewMovieVM, Movie>(movie);

            await _context.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            // Them danh sach DV
            foreach (var actorId in movie.ActorIds)
            {
                var movieActor = new Movie_Actor()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };

                await _context.Movies_Actors.AddAsync(movieActor);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovieSync(int id, NewMovieVM movie)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (dbMovie != null)
            {
                var updateMovie = _mapper.Map<NewMovieVM, Movie>(movie);

                _context.Entry(dbMovie).CurrentValues.SetValues(updateMovie);

                await _context.SaveChangesAsync();

                // Remove existing Actors
                var existingActors = await _context.Movies_Actors.Where(am => am.MovieId == id).ToListAsync();
                _context.Movies_Actors.RemoveRange(existingActors);
                await _context.SaveChangesAsync();

                // Add Actors
                foreach (var actorId in movie.ActorIds)
                {
                    var actorMovie = new Movie_Actor()
                    {
                        MovieId = id,
                        ActorId = actorId
                    };
                    _context.Movies_Actors.Add(actorMovie);
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Movie> GetMovieByIdAsyn(int id)
        {
            var movieDetails = await _context.Movies
                .Include(c => c.Cimena)
                .Include(p => p.Producer)
                .Include(c => c.Category)
                .Include(ma => ma.Movies_Actors).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);

            return movieDetails;
        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsVM()
        {
            var response = new NewMovieDropdownsVM()
            {
                Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync(),
                Actors = await _context.Actors.OrderBy(a => a.Name).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(c => c.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(p => p.Name).ToListAsync()
            };
            return response;
        }
    }
}