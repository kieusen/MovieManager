using AutoMapper;
using Movie_01.Data.ViewModels;
using Movie_01.Models;

namespace Movie_01.Data.Helpers
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