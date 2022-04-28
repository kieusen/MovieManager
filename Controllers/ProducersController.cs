using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie_01.Data.Helpers;
using Movie_01.Data.Services;
using Movie_01.Models;

namespace Movie_01.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProducersController : Controller
    {
        private string folder_image = "producers";
        private readonly IProducersService _producersService;
        public ProducersController(IProducersService producersService)
        {
            _producersService = producersService;
        }

        //Danh sach        
        public async Task<IActionResult> Index(int page = 1)
        {
            var producers = await _producersService.GetAllAsync(page);
            ViewBag.Pager = new Pager(producers.TotalItems, producers.PageIndex, producers.PageSize, "Producers", "Index");
            
            return View(producers);
        }

        // Chi tiet
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var producer = await _producersService.GetByIdAsync(id);

            if (producer != null) return View(producer);

            return View("NotFound");
        }

        // Them
        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producer producer)
        {            
            if (!ModelState.IsValid) 
            {
                return View(producer);
            }            

            producer.ProfilePictureURL = await _producersService.UploadFileAsync(producer.ProfilePicture, folder_image);

            await _producersService.AddAsync(producer);

            return RedirectToAction(nameof(Index));
        }

        // Sua
        public async Task<IActionResult> Edit(int id)
        {
            var producer = await _producersService.GetByIdAsync(id);

            if (producer != null) return View(producer);

            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Producer producer)
        {
            if (id != producer.Id) return View("NotFound");

            ModelState.Remove("ProfilePicture");
            if (!ModelState.IsValid) return View(producer);

            if (producer.ProfilePicture != null)
                producer.ProfilePictureURL = await _producersService.UploadFileAsync(producer.ProfilePicture, folder_image);

            await _producersService.UpdateAsync(id, producer);

            return RedirectToAction(nameof(Index));
        }

        // Xoa        
        public async Task<IActionResult> Delete(int id, int page = 1)
        {
            await _producersService.DeleteAsync(id);

            return RedirectToAction(nameof(Index), new {page = page});
        }
    }
}