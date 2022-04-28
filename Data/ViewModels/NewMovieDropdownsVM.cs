using Movie_01.Models;

namespace Movie_01.Data.ViewModels
{
    public class NewMovieDropdownsVM
    {
        public NewMovieDropdownsVM()
        {
            Categories = new List<Category>();
            Actors = new List<Actor>();
            Cinemas = new List<Cinema>();
            Producers = new List<Producer>();
        }

        public List<Category> Categories { get; set; }
        public List<Actor> Actors { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public List<Producer> Producers { get; set; }
    }
}