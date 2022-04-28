using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Movie_01.Data.Helpers;

namespace Movie_01.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public EntityBaseRepository(AppDbContext context, IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>()
                .FirstOrDefaultAsync(n => n.Id == id);

            EntityEntry entry = _context.Entry<T>(entity);
            entry.State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.ToListAsync();
        }

        public async Task<PaginatedList<T>> GetAllAsync(int pageIndex)
        {            
            int pageSize = 5;                        
            return await PaginatedList<T>.CreateAsync(_context.Set<T>().AsQueryable(), pageIndex, pageSize);
        }

        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task UpdateAsync(int id, T entity)
        {
            EntityEntry entry = _context.Entry<T>(entity);
            entry.State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {        
            var uploadDir = "images";
            var uploadPath = Path.Combine(_env.WebRootPath, uploadDir, folderName);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = folderName + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}