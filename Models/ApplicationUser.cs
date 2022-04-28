using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MovieManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Tên người dùng")]
        public string FullName { get; set; }
    }
}