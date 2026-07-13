using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<List<T>> getAllAsync(string[]? includes =null)  
        {
            IQueryable<T> query = _context.Set<T>();//nothing will return to the user ,its on the server side
            if (includes != null) 
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);//we can add multiple includes to the query becouse the query is an IQueryable and we can add multiple includes to it
                }
            }
            return await query.ToListAsync();
        }
        public async Task<T> createAsync(T entity) 
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
