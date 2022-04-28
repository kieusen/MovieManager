using MovieManager.Data.Base;
using MovieManager.Models;

namespace MovieManager.Data.Services
{
    public class ProducersService : EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(AppDbContext context, IWebHostEnvironment env) : base(context, env)
        {
        }
    }
}