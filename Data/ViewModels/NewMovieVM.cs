using System.ComponentModel.DataAnnotations;

namespace MovieManager.Data.ViewModels
{
    public class NewMovieVM
    {
       public int Id { get; set; }
        
        [Display(Name = "Tên phim")]
        [Required(ErrorMessage ="{0} không được để trống")]
        public string Name { get; set; }

        public string ImageURL { get; set; }

        [Display(Name = "Hình ảnh")]
        [Required(ErrorMessage = "{0} chưa được chọn")]
        public IFormFile Image { get; set; }

        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "{0} không được để trống")]
        public string Description { get; set; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "{0} không được để trống")]
        public double Price { get; set; }

        [Display(Name = "Khởi chiếu")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Kết thúc")]        
        public DateTime EndDate { get; set; }
       
        [Display(Name = "Chọn rạp")]                
        [Required(ErrorMessage = "Rạp chưa được chọn")]
        public int CinemaId { get; set; }

        [Display(Name = "Chọn nhà sản xuất")]
        [Required(ErrorMessage = "Nhà sản xuất chưa được chọn")]
        public int ProducerId { get; set; }       

        [Display(Name = "Chọn thể loại")]
        [Required(ErrorMessage = "Thể loại chưa được chọn")]
        public int CategoryId { get; set; }

        [Display(Name = "Chọn diễn viên")]
        [Required(ErrorMessage = "Diễn viên chưa được chọn")]
        public List<int> ActorIds { get; set; }                      
    }
}