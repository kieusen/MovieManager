using Microsoft.EntityFrameworkCore;
using MovieManager.Data.Helpers;
using MovieManager.Models;

namespace MovieManager.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;
        public OrdersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Order>> GetOrdersByUserInAnRoleAsync(string userId, string userRole, int page)
        {
            var orders = _context.Orders
                .Include(o => o.OrderItems).ThenInclude(o => o.Movie)
                .Include(o => o.User)
                .AsQueryable();

            if (userRole != UserRoles.Admin)
            {
                orders = orders.Where(o => o.UserId == userId);
            }

            var pageSize = 5;
            var ordersPaging = await PaginatedList<Order>.CreateAsync(orders, page, pageSize);

            return ordersPaging;
        }

        public async Task StoreOrder(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    Price = item.Movie.Price,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}