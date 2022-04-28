using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Movie_01.Data.Base;

namespace Movie_01.Models
{
    public class Cinema : IEntityBase
    {
        public int Id { get; set; }
        
        [Display(Name = "Rạp")]
        [Required(ErrorMessage = "{0} không được để trống")]
        public string Name { get; set; }    
        public string LogoURL { get; set; }

        [Display(Name = "Logo")]
        [Required(ErrorMessage = "{0} chưa được chọn")]    
        [NotMapped]
        public IFormFile Logo  { get; set; }

        public List<Movie> Movies { get; set; }
    }
}