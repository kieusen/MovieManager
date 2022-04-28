using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Movie_01.Data.Base;

namespace Movie_01.Models
{
    public class Actor : IEntityBase
    {
        public int Id { get; set; }
        
        [Display(Name = "Tên diễn viên")]
        [Required(ErrorMessage ="{0} không được để trống")]
        public string Name { get; set; }

        public string ProfilePictureURL { get; set; }

        [Display(Name = "Ảnh diễn viên")]
        [Required(ErrorMessage = "{0} chưa được chọn")]        
        [NotMapped]
        public IFormFile ProfilePicture { get; set; }

        [Display(Name = "Mô tả")]        
        public string Bio { get; set; }

        public List<Movie_Actor> Movies_Actors { get; set; }
    }
}