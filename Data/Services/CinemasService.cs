using MovieManager.Data.Base;
using MovieManager.Models;

namespace MovieManager.Data.Services
{
    public class CinemasService : EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(AppDbContext context, IWebHostEnvironment env) : base(context, env)
        {
        }
    }
}