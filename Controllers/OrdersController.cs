using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Data.Cart;
using MovieManager.Data.Helpers;
using MovieManager.Data.Services;
using MovieManager.Data.ViewModels;

namespace MovieManager.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _ordersService = ordersService;
            _shoppingCart = shoppingCart;
            _moviesService = moviesService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.Success = 0;

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var order = await _ordersService.GetOrdersByUserInAnRoleAsync(userId, userRole, page);
            ViewBag.Pager = new Pager(order.TotalItems, order.PageIndex, order.PageSize, "Orders", "Index");

            return View(order);
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();

            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        public async Task<IActionResult> AddToShoppingCart(int id, string contr, string act)
        {
            var item = await _moviesService.GetMovieByIdAsyn(id);
            if (item != null)            
            {
                await _shoppingCart.AddItemToCartAsync(item);
            }
            return RedirectToAction(nameof(ShoppingCart));               
        }

        public async Task<IActionResult> RemoveFromShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsyn(id);
            if (item != null)
            {
                await _shoppingCart.RemoveItemFromCartAsync(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrder(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            ViewBag.Success = 1;
            return View(nameof(ShoppingCart));            
        }
    }
}