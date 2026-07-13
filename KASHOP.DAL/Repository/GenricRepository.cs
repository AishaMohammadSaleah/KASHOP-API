using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repository
{
    public class GenricRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenricRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        private IQueryable<T> ApplyIncludes(IQueryable<T> query, string[]? includes)
        {
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }
        public async Task<List<T>> getAllAsync(string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            query = ApplyIncludes(query, includes);
            return await query.ToListAsync();
        }
        public async Task<T> createAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> GetOne(Expression<Func<T, bool>> filter, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            query = ApplyIncludes(query, includes);
            return await query.FirstOrDefaultAsync(filter);

        }
    }
}
