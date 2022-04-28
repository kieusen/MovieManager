using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_01.Data;
using Movie_01.Data.Cart;
using Movie_01.Data.Helpers;
using Movie_01.Data.ViewModels;
using Movie_01.Models;

namespace Movie_01.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly ShoppingCart _shoppingCart;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context, ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // Dang nhap
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            // Lay user theo email
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            // Co user
            if (user != null)
            {
                // Kiem tra mat khau
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                    if (result.Succeeded) return RedirectToAction("Index", "Movies");
                }

                TempData["Error"] = "Đăng nhập không thành công";
                return View(loginVM);
            }

            TempData["Error"] = "Email chưa được đăng ký";
            return View(loginVM);
        }

        // Dang xuat
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await _shoppingCart.ClearShoppingCartAsync();

            return RedirectToAction("Index", "Movies");
        }

        // Dang ky
        public IActionResult Register()
        {
            ViewBag.Success = 0;
            return View(new RegisterVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            List<string> passwordErrors = new List<string>();

            var validators = _userManager.PasswordValidators;

            foreach (var validator in validators)
            {
                var result = await validator.ValidateAsync(_userManager, null, registerVM.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        passwordErrors.Add(error.Description);
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);

            if (user != null)
            {
                TempData["Error"] = "Email đã được đăng ký";
                return View(registerVM);
            }

            // Tao user moi
            var newUser = new ApplicationUser
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            // Them role cho user
            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);


            ViewBag.Success = 1;
            return View();
        }

        public IActionResult AccessDenied(string ReturnUrl) => View();
    }
}