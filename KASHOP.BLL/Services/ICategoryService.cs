using KASHOP.DAL.Dto;
using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services
{
    public interface ICategoryService
    {
        Task <List<CategoryResponse>> GetAllCategories();
        Task <CategoryResponse> CreateCategory(CategoryRequest categoryRequest);
        Task<CategoryResponse> GetCategory (Expression<Func<Category, bool>> filtter);
    }
}
