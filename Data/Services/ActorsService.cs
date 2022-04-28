using MovieManager.Data.Base;
using MovieManager.Models;

namespace MovieManager.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(AppDbContext context, IWebHostEnvironment env) : base(context, env)
        {
        }
    }
}