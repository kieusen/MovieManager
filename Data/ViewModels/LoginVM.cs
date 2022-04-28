using System.ComponentModel.DataAnnotations;

namespace Movie_01.Data.ViewModels 
{
    public class LoginVM 
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} chưa được nhập")]
        [EmailAddress(ErrorMessage = "{0} không hợp lệ")]
        public string EmailAddress { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} chưa được nhập")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}