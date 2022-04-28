using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Movie_01.Data.Helpers;
using Movie_01.Data.Services;
using Movie_01.Models;

namespace Movie_01.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        // Danh sach        
        public async Task<IActionResult> Index(int page = 1)
        {
            var categories = await _categoriesService.GetAllAsync(page);
            ViewBag.Pager = new Pager(categories.TotalItems, categories.PageIndex, categories.PageSize, "Categories", "Index");

            return View(categories);
        }

        // Chi tiet
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var category = await _categoriesService.GetByIdAsync(id);

            if (category != null) return View(category);

            return View("NotFound");
        }

        // Them
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")] Category category)
        {
            if (!ModelState.IsValid) return View(category);

            await _categoriesService.AddAsync(category);

            return RedirectToAction(nameof(Index));
        }

        // Sua
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoriesService.GetByIdAsync(id);

            if (category != null) return View(category);

            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit (int id, Category category)
        {
            if (id != category.Id) return View("NotFound");

            if (!ModelState.IsValid) return View(category);

            await _categoriesService.UpdateAsync(id, category);

            return RedirectToAction(nameof(Index));
        }

        // Xoa        
        public async Task<IActionResult> Delete(int id, int page = 1)
        {           
            await _categoriesService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), new {page = page});
        }
    }
}