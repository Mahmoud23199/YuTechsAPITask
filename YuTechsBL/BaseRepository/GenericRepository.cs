using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YuTechsBL.Repository;
using YuTechsEF.Context;

namespace YuTechsBL.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly YuAppDbContext _context;

        public GenericRepository(YuAppDbContext context)
        {
            this._context= context;
        }


        public async Task DeleteAsync(T entity)
        {
             _context.Set<T>().Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(string[] includes=null)
        {
            IQueryable<T> query= _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes) {

                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {

                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(match);
        }

        public async Task<T> GetByNameAsync(Expression<Func<T,bool>>match, string[] includes = null)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(match);
        }

        public async Task UpdateAsync(Expression<Func<T, bool>> match, T entity)
        {
            var item = await GetByIdAsync(match);

            if (item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(entity);

                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteByIdAsync(Expression<Func<T, bool>> match)
        {
            var item = await GetByIdAsync(match);

            if (item != null)
            {
                _context.Set<T>().Remove(item);
                await _context.SaveChangesAsync();

            }
        }

        public async Task AddedAsync(T entity)
        {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();

        }
    }
}
