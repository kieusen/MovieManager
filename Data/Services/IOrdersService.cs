using Movie_01.Data.Helpers;
using Movie_01.Models;

namespace Movie_01.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrder(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<PaginatedList<Order>> GetOrdersByUserInAnRoleAsync(string userId, string userRole, int page);

    }
}