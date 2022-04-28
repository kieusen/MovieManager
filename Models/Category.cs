using System.ComponentModel.DataAnnotations;
using MovieManager.Data.Base;

namespace MovieManager.Models
{
    public class Category : IEntityBase
    {
        public int Id { get; set; }
        
        [Display(Name = "Tên thể loại")]
        [Required(ErrorMessage = "{0} không được để trống")]
        public string Name { get; set; }

        public List<Movie> Movies {get; set;}
    }
}