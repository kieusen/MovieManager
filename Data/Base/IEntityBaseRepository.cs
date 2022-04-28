using System.Linq.Expressions;
using MovieManager.Data.Helpers;

namespace MovieManager.Data.Base
{
    public interface IEntityBaseRepository <T> where T: class, IEntityBase, new ()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<PaginatedList<T>> GetAllAsync(int pageIndex);

        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);

        Task AddAsync (T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);    
        Task<string> UploadFileAsync(IFormFile file, string folderName);
    }
}