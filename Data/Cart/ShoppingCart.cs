using Microsoft.EntityFrameworkCore;
using Movie_01.Models;

namespace Movie_01.Data.Cart
{
    public class ShoppingCart
    {
        private readonly AppDbContext _context;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public List<ShoppingCartItem> GetShoppingCartItems() => ShoppingCartItems ?? 
        (ShoppingCartItems = _context.ShoppingCartItems
            .Where(s => s.ShoppingCartId == ShoppingCartId)
            .Include(s => s.Movie)
            .ToList());

        public double GetShoppingCartTotal() =>
        _context.ShoppingCartItems
            .Where(s => s.ShoppingCartId == ShoppingCartId)
            .Select(s => s.Movie.Price * s.Amount)
            .Sum();

        public async Task AddItemToCartAsync(Movie movie)
        {
            var item = await _context.ShoppingCartItems
                .FirstOrDefaultAsync(s => s.ShoppingCartId == ShoppingCartId && s.Movie.Id == movie.Id);

            if (item == null)
            {
                item = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };
                await _context.ShoppingCartItems.AddAsync(item);
            }
            else
            {
                item.Amount ++;
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemFromCartAsync(Movie movie)
        {
            var item = await _context.ShoppingCartItems
                .FirstOrDefaultAsync(s => s.ShoppingCartId == ShoppingCartId && s.Movie.Id == movie.Id);

            if (item != null)
            {
                if (item.Amount > 1)
                    item.Amount --;
                else
                    _context.ShoppingCartItems.Remove(item);
            }

            await _context.SaveChangesAsync();
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .ToListAsync();

            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();

            ShoppingCartItems = new List<ShoppingCartItem>();
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = service.GetService<AppDbContext>();

            string cartId = session.GetString("cartId") ?? Guid.NewGuid().ToString();
            session.SetString("cartId", cartId);

            return new ShoppingCart(context)
            {
                ShoppingCartId = cartId
            };
        }
    }
}