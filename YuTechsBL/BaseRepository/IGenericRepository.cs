using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace YuTechsBL.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddedAsync(T entity);

        Task<T> GetByIdAsync(Expression<Func<T, bool>> match, string[] includes = null);

        Task<T> GetByNameAsync(Expression<Func<T, bool>> match, string[] includes = null);

        Task<IEnumerable<T>> GetAllAsync(string[] includes);

        Task UpdateAsync(Expression<Func<T, bool>> match, T entity);

        Task DeleteAsync(T entity);
        Task DeleteByIdAsync(Expression<Func<T, bool>> match);

    }
}
