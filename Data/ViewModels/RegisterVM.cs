using System.ComponentModel.DataAnnotations;

namespace MovieManager.Data.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} chưa được nhập")]
        [EmailAddress(ErrorMessage = "{0} không hợp lệ")]
        public string EmailAddress { get; set; }

        [Display(Name = "Tên đầy đủ")]
        [Required(ErrorMessage = "{0} chưa được nhập")]
        public string FullName { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "{0} chưa được nhập")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Nhập lại mật khẩu")]
        [Required(ErrorMessage = "Chưa nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "{0} chưa đúng")]
        public string ConfirmPassword { get; set; }
    }
}