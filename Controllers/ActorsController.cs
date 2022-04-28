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
    public class ActorsController : Controller
    {
        private string folder_image = "actors";
        private readonly IActorsService _actorsService;
        public ActorsController(IActorsService actorsService)
        {
            _actorsService = actorsService;
        }

        // Danh sach        
        public async Task<IActionResult> Index(int page = 1)        
        {
            var actors = await _actorsService.GetAllAsync(page);
            ViewBag.Pager = new Pager(actors.TotalItems, actors.PageIndex, actors.PageSize, "Actors", "Index");
            
            return View(actors);
        }

        // Chi tiet
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var actor = await _actorsService.GetByIdAsync(id);

            if (actor != null)
                return View(actor);

            return View("NotFound");
        }


        // Them
        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Actor newActor)
        {
            if (!ModelState.IsValid) 
            {
                return View(newActor);
            }            

            newActor.ProfilePictureURL = await _actorsService.UploadFileAsync(newActor.ProfilePicture, folder_image);

            await _actorsService.AddAsync(newActor);

            return RedirectToAction(nameof(Index));
        }

        // Sua
        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _actorsService.GetByIdAsync(id);

            if (actor != null) return View(actor);

            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (id != actor.Id) return View("NotFound");

            ModelState.Remove("ProfilePicture");
            if (!ModelState.IsValid) return View(actor);

            if (actor.ProfilePicture != null)
                actor.ProfilePictureURL = await _actorsService.UploadFileAsync(actor.ProfilePicture, folder_image);

            await _actorsService.UpdateAsync(id, actor);

            return RedirectToAction(nameof(Index));
        }

        // Xoa
        public async Task<IActionResult> Delete(int id, int page = 1)
        {
            await _actorsService.DeleteAsync(id);

            return RedirectToAction(nameof(Index), new {page = page});
        }
    }
}