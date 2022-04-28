using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Data.Helpers;
using MovieManager.Data.Services;
using MovieManager.Models;

namespace MovieManager.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemasController : Controller
    {
        private string folder_image = "cinemas";

        private readonly ICinemasService _cinemasService;
        public CinemasController(ICinemasService cinemasService)
        {
            _cinemasService = cinemasService;
        }

        //Danh sach        
        public async Task<IActionResult> Index(int page = 1)
        {
            var cinemas = await _cinemasService.GetAllAsync(page);
            ViewBag.Pager = new Pager(cinemas.TotalItems, cinemas.PageIndex, cinemas.PageSize, "Cinemas", "Index");
            
            return View(cinemas);
        }        

        // Chi tiet
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var cinema = await _cinemasService.GetByIdAsync(id);

            if (cinema != null) return View(cinema);

            return View("NotFound");
        }

        // Them
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cinema cinema)
        {
            if (!ModelState.IsValid) 
                return View(cinema);

            cinema.LogoURL = await _cinemasService.UploadFileAsync(cinema.Logo, folder_image);

            await _cinemasService.AddAsync(cinema);

            return RedirectToAction(nameof(Index));
        }

        // Sua
        public async Task<IActionResult> Edit(int id)
        {
            var cinema = await _cinemasService.GetByIdAsync(id);

            if (cinema != null) return View(cinema);

            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cinema cinema)
        {
            if (id != cinema.Id) return View("NotFound");

            ModelState.Remove("Logo");

            if (!ModelState.IsValid) return View(cinema);

            if (cinema.Logo != null)
                cinema.LogoURL = await _cinemasService.UploadFileAsync(cinema.Logo, folder_image);

            await _cinemasService.UpdateAsync(id, cinema);

            return RedirectToAction(nameof(Index));
        }

        // Xoa
        public async Task<IActionResult> Delete(int id, int page = 1)
        {
            await _cinemasService.DeleteAsync(id);

            return RedirectToAction(nameof(Index), new {page = page});
        }
    }
}