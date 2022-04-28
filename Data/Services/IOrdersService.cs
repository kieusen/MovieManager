using MovieManager.Data.Helpers;
using MovieManager.Models;

namespace MovieManager.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrder(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<PaginatedList<Order>> GetOrdersByUserInAnRoleAsync(string userId, string userRole, int page);

    }
}