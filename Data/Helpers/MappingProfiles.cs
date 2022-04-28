using AutoMapper;
using MovieManager.Data.ViewModels;
using MovieManager.Models;

namespace MovieManager.Data.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Source -> Target
            CreateMap<NewMovieVM, Movie>();
            CreateMap<Movie, NewMovieVM>();
        }
    }
}