using Movie_01.Data.Base;
using Movie_01.Models;

namespace Movie_01.Data.Services
{
    public class ProducersService : EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(AppDbContext context, IWebHostEnvironment env) : base(context, env)
        {
        }
    }
}