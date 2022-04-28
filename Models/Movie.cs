using System.ComponentModel.DataAnnotations;
using Movie_01.Data.Base;

namespace Movie_01.Models
{
    public class Movie : IEntityBase
    {
        public int Id { get; set; }
        
        [Display(Name = "Tên phim")]
        [Required(ErrorMessage ="{0} không được để trống")]
        public string Name { get; set; }

        [Display(Name = "Hình ảnh")]
        [Required(ErrorMessage = "{0} không được để trống")]
        public string ImageURL { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Giá")]
        public double Price { get; set; }

        [Display(Name = "Ngày chiếu")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Ngày kết thúc")]        
        public DateTime EndDate { get; set; }

        public int CinemaId { get; set; }
        [Display(Name = "Rạp")]                
        public Cinema Cimena { get; set; }

        public int ProducerId { get; set; }
        [Display(Name = "Nhà sản xuất")]
        public Producer Producer { get; set; }


        public int CategoryId { get; set; }
        [Display(Name = "Thể loại")]
        public Category Category { get; set; }


        public List<Movie_Actor> Movies_Actors { get; set; }        
    }
}