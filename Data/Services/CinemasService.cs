using Movie_01.Data.Base;
using Movie_01.Models;

namespace Movie_01.Data.Services
{
    public class CinemasService : EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(AppDbContext context, IWebHostEnvironment env) : base(context, env)
        {
        }
    }
}