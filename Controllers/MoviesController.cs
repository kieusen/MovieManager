using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieManager.Data.Helpers;
using MovieManager.Data.Services;
using MovieManager.Data.ViewModels;
using MovieManager.Models;

namespace MovieManager.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly IMapper __mapper;
        public MoviesController(IMoviesService moviesService, IMapper _mapper)
        {
            __mapper = _mapper;
            _moviesService = moviesService;
        }

        public async Task LoadDropdownsList()
        {
            var dropdowns = await _moviesService.GetNewMovieDropdownsVM();

            ViewBag.Categories = new SelectList(dropdowns.Categories, "Id", "Name");
            ViewBag.Actors = new SelectList(dropdowns.Actors, "Id", "Name");
            ViewBag.Cinemas = new SelectList(dropdowns.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(dropdowns.Producers, "Id", "Name");
        }

        // Danh sach
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var movies = await _moviesService.GetAllAsync(c => c.Category, c => c.Cimena, p => p.Producer);

            return View(movies);
        }

        // Chi tiet
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var movie = await _moviesService.GetMovieByIdAsyn(id);

            if (movie != null) return View(movie);

            return View("NotFound");
        }

        // Them
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM newMovie)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsList();
                return View(newMovie);
            }

            newMovie.ImageURL = await _moviesService.UploadFileAsync(newMovie.Image, "movies"); 

            await _moviesService.AddMovieSync(newMovie);
            return RedirectToAction(nameof(Index));
        }

        // Sua
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _moviesService.GetMovieByIdAsyn(id);

            if (movie != null)
            {
                var newMovie = __mapper.Map<Movie, NewMovieVM>(movie);
                // Danh sach dien vien
                newMovie.ActorIds = movie.Movies_Actors.Select(ma => ma.ActorId).ToList();
                await LoadDropdownsList();
                return View(newMovie);
            }

            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM newMovie)
        {
            if (id != newMovie.Id) return View("NotFound");

            ModelState.Remove("Image");
            if (!ModelState.IsValid)
            {
                await LoadDropdownsList();
                return View(newMovie);
            }

            if (newMovie.Image != null)
                newMovie.ImageURL = await _moviesService.UploadFileAsync(newMovie.Image, "movies");           

            await _moviesService.UpdateMovieSync(id, newMovie);
            return RedirectToAction(nameof(Index));
        }        

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Filter(string searchString = "")
        {
            var movie = await _moviesService.GetAllAsync(c => c.Category, c => c.Cimena, c => c.Producer);

            if(!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                var movieFilter = movie.Where(m => m.Name.ToLower().Contains(searchString) || 
                                              m.Description.ToLower().Contains(searchString)).ToList();
                                              
                return View(nameof(Index), movieFilter);
            }

            return View(nameof(Index), movie);
        }
    }
}