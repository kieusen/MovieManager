using Movie_01.Data.Base;
using Movie_01.Models;

namespace Movie_01.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(AppDbContext context, IWebHostEnvironment env) : base(context, env)
        {
        }
    }
}