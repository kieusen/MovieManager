using Movie_01.Data.Base;
using Movie_01.Models;

namespace Movie_01.Data.Services
{
    public class CategoriesService : EntityBaseRepository<Category>, ICategoriesService
    {
        public CategoriesService(AppDbContext context) : base(context)
        {
        }
    }
}