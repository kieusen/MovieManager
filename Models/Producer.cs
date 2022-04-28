using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieManager.Data.Base;

namespace MovieManager.Models
{
    public class Producer : IEntityBase
    {
        public int Id { get; set; }
        
        [Display(Name = "Nhà sản xuất")]
        [Required(ErrorMessage ="{0} không được để trống")]
        public string Name { get; set; }

        public string ProfilePictureURL { get; set; }

        [Display(Name = "Ảnh nhà sản xuất")]
        [Required(ErrorMessage = "{0} chưa được chọn")]        
        [NotMapped]
        public IFormFile ProfilePicture { get; set; }        

        [Display(Name = "Mô tả")]        
        public string Bio { get; set; }

        public List<Producer> Producers { get; set; }
    }
}