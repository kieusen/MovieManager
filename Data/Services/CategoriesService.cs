using MovieManager.Data.Base;
using MovieManager.Models;

namespace MovieManager.Data.Services
{
    public class CategoriesService : EntityBaseRepository<Category>, ICategoriesService
    {
        public CategoriesService(AppDbContext context) : base(context)
        {
        }
    }
}