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
    public class CategoryRepository: ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Category> getAll()
        {
            return _context.Categories.Include(c => c.Translations).ToList();

        }

        Category ICategoryRepository.create(Category category)
        {
           _context.Add(category);
            _context.SaveChanges();
            return category;
        }

       
    }
}
